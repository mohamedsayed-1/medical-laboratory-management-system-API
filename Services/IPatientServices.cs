using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IPatientServices
    {
        public Patient? FindByPhoneNumber(string phoneNumber);
        public List<PatientDetailsDTO> GetAll(int pageNum, int pageSize);
        public PatientDetailsDTO? GetById(int id);
        public PatientDetailsDTO? GetByPhoneNumber(string phoneNumber);
    }
}
