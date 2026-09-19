using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface ILabTestServices
    {
        public List<LabTest> GetAllWithIds(List<int> ids);
        public List<LabTestDetailsDTO> GetAll(int pageNum, int pageSize);
        public LabTestDetailsDTO? GetById(int id);
        public int Add(AddLabTestDTO labTestDTO);
        public int? Edit(int id, EditLabTestDTO labTestDTO);
        public int? Delete(int id);
    }
}
