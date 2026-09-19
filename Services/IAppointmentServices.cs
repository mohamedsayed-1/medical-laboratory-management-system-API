using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IAppointmentServices
    {
        public Appointment? Add(AddAppointmentDTO appointmentDTO);
        public AppointmentDetailsDTO? GetByIdWithIncludes(int id);
        public List<AppointmentDetailsDTO> GetAllWithIncludes(int pageNum, int pageSize);
        public List<AppointmentDetailsDTO>? GetAllWithIncludesFilterByStatus(int pageNum, int pageSize, string status);
        public List<AppointmentDetailsDTO>? GetAllByPhoneNumberWithIncludes(int pageNum, int pageSize, string? status, string phoneNumber);
    }
}
