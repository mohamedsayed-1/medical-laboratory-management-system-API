using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface ILabTestServices
    {
        public List<LabTest> GetAllWithIds(List<int> ids);
    }
}
