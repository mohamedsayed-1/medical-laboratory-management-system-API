using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.DTOs
{
    public class AppointmentDetailsDTO
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        public AppointmentStatus Status { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientPhoneNumber { get; set; } = string.Empty;
        public string? PatientEmail { get; set; }
        public MaritalStatus? PatientMaritalStatus { get; set; }
        public Gender PatientGender { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public List<RequestedLabTestDetailsDTO> RequestedLabTests { get; set; } = []; // didn't decide yet if i want to send this with the request, i did that in the mvc but i don't know the api best practices
    }
}
