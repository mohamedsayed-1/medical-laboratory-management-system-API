using Medical_Laboratory_Management_System.DTOs;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IRequestedLabTestServices
    {
        public List<RequestedLabTestDetailsDTO> GetAll(int pageNum, int pageSize);
        public List<RequestedLabTestDetailsDTO>? GetAllByStatus(int pageNum, int pageSize, string status);
        public RequestedLabTestDetailsDTO? GetById(int id);
    }
}
