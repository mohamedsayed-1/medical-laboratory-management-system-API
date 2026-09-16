using Medical_Laboratory_Management_System.DTOs;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IAppointmentServices
    {
        public bool Add(AddAppointmentDTO appointmentDTO);
    }
}
