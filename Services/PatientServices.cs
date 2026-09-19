using System.Linq.Expressions;
using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly MLMSDbContext context;

        public PatientServices(MLMSDbContext context)
        {
            this.context = context;
        }
        public Patient? FindByPhoneNumber(string phoneNumber)
        {
            return context.Patients.Where(x => x.PhoneNumber == phoneNumber).SingleOrDefault(); // single not first as i want to make very sure that there's only one phone number
        }

        public List<PatientDetailsDTO> GetAll(int pageNum, int pageSize)
        {
            var patients = context.Patients
                .Select(ToPatientDetailsDTO())
                .OrderBy(x => x.PatientId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return patients;

        }

        public PatientDetailsDTO? GetById(int id)
        {
            var patient = context.Patients
                .Where(x => x.Id == id)
                .Select(ToPatientDetailsDTO())
                .FirstOrDefault();
            return patient;
        }

        private Expression<Func<Patient, PatientDetailsDTO>> ToPatientDetailsDTO()
        {
            return x => new PatientDetailsDTO()
            {
                PatientId = x.Id,
                PatientName = x.Name,
                PatientDateOfBirth = x.DateOfBirth,
                PatientEmail = x.Email,
                PatientGender = x.Gender,
                PatientMaritalStatus = x.MaritalStatus,
                PatientPhoneNumber = x.PhoneNumber,
                Appointments = x.Appointments.Select(y => new SummarizedAppointmentDetailsDTO()
                {
                    AppointmentId = y.Id,
                    Date = y.Date,
                    Status = y.Status,
                    Urgent = y.Urgent
                }).ToList()
            };
        }
    }
}
