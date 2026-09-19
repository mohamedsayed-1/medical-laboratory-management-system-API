using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.DTOs
{
    public class SummarizedAppointmentDetailsDTO
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public bool Urgent { get; set; }
        public AppointmentStatus Status { get; set; }

    }
}
