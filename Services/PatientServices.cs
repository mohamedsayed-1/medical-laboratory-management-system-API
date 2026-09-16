using Medical_Laboratory_Management_System.Data;
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
    }
}
