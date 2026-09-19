using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface ILabTestServices
    {
        public List<LabTest> GetAllWithIds(List<int> ids);
        public List<LabTestDetailsDTO> GetAll();
    }
}
