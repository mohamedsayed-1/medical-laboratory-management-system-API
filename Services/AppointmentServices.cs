using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

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
        public bool Add(AddAppointmentDTO appointmentDTO)
        {
            List<LabTest> labTests = labTestServices.GetAllWithIds(appointmentDTO.LabTestsIds);
            if (labTests.Count != appointmentDTO.LabTestsIds.Count)
                return false;
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
            return true;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
