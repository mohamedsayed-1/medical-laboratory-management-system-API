using System.Linq.Expressions;
using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly MLMSDbContext context;
        private readonly IPatientServices patientServices;
        private readonly ILabTestServices labTestServices;

        public AppointmentServices(MLMSDbContext context,
            IPatientServices patientServices,
            ILabTestServices labTestServices)
        {
            this.context = context;
            this.patientServices = patientServices;
            this.labTestServices = labTestServices;
        }
        public Appointment? Add(AddAppointmentDTO appointmentDTO)
        {
            List<LabTest> labTests = labTestServices.GetAllWithIds(appointmentDTO.LabTestsIds);
            if (labTests.Count != appointmentDTO.LabTestsIds.Count)
                return null;
            var patient = patientServices.FindByPhoneNumber(appointmentDTO.PatientPhoneNumber);
            if (patient is null)
            {
                patient = new Patient()
                {
                    Name = appointmentDTO.PatientName,
                    PhoneNumber = appointmentDTO.PatientPhoneNumber,
                    DateOfBirth = appointmentDTO.PatientDateOfBirth,
                    Email = appointmentDTO.PatientEmail,
                    Gender = appointmentDTO.PatientGender,
                    MaritalStatus = appointmentDTO.PatientMaritalStatus,
                };
                context.Patients.Add(patient);
            }
            var newAppointment = Appointment.Create(appointmentDTO.Date, appointmentDTO.Notes, appointmentDTO.Urgent, patient);
            foreach (var labTest in labTests)
            {
                context.RequestedLabTests.Add(RequestedLabTest.Create(newAppointment, labTest));
            }
            context.Appointments.Add(newAppointment);
            Save();
            return newAppointment;
        }

        public AppointmentDetailsDTO? GetByIdWithIncludes(int id)
        {
            var appointment = context.Appointments
                .Select(ToAppointmentDetailsDTO())
                .FirstOrDefault(x => x.AppointmentId == id);
            if (appointment is null)
                return null;
            return appointment;
        }

        public List<AppointmentDetailsDTO> GetAllWithIncludes(int pageNum, int pageSize)
        {
            var appointments = context.Appointments
                .Select(ToAppointmentDetailsDTO())
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.AppointmentId)
                .ToList();
            return appointments;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<AppointmentDetailsDTO>? GetAllWithIncludesFilterByStatus(int pageNum, int pageSize, string status)
        {
            IQueryable<Appointment> appointments;
            if (status == null)
                return null;

            if (status.Equals("deleted", StringComparison.OrdinalIgnoreCase))
            {
                appointments = context.Appointments
                .IgnoreQueryFilters()
                .Where(x => x.IsDeleted);
            }
            else
            {
                appointments = context.Appointments
                .Where(x => x.Status.ToString().ToUpper() == status.ToUpper());
            }
            var appointmentsDTO = appointments.Select(ToAppointmentDetailsDTO())
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.AppointmentId)
            .ToList();
            return appointmentsDTO;
        }
        private Expression<Func<Appointment,AppointmentDetailsDTO>> ToAppointmentDetailsDTO()
        {
            return x => new AppointmentDetailsDTO()
            {
                Date = x.Date,
                Notes = x.Notes,
                Urgent = x.Urgent,
                Status = x.Status,
                AppointmentId = x.Id,
                PatientName = x.Patient.Name,
                PatientDateOfBirth = x.Patient.DateOfBirth,
                PatientEmail = x.Patient.Email,
                PatientGender = x.Patient.Gender,
                PatientMaritalStatus = x.Patient.MaritalStatus,
                PatientPhoneNumber = x.Patient.PhoneNumber,
                RequestedLabTests = x.RequestedLabTests.Select(y => new RequestedLabTestDetailsDTO()
                {
                    RequestedLabTestId = y.Id,
                    RequestedLabTestStatus = y.Status,
                    LabTestResultNotes = y.LabTestResult != null ? y.LabTestResult.Notes : null,
                    LabTestResultValue = y.LabTestResult != null ? y.LabTestResult.Value : null,
                    LabTestName = y.LabTest.Name,
                    LabTestPrice = y.LabTest.Price
                }).ToList()
            };
        }

        public List<AppointmentDetailsDTO>? GetAllByPhoneNumberWithIncludes(int pageNum, int pageSize, string? status, string phoneNumber)
        {
            IQueryable<Appointment> appointments;
            if (status == null)
                return null;

            if (status.Equals("deleted", StringComparison.OrdinalIgnoreCase))
            {
                appointments = context.Appointments
                .IgnoreQueryFilters()
                .Where(x => x.IsDeleted);
            }
            else
            {
                appointments = context.Appointments
                .Where(x => x.Status.ToString().ToUpper() == status.ToUpper());
            }
            var appointmentsDTO = appointments.Select(ToAppointmentDetailsDTO())
                .Where(x => x.PatientPhoneNumber == phoneNumber)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.AppointmentId)
                .ToList();
            return appointmentsDTO;
        }
    }
}
