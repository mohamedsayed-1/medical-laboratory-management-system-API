using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.DTOs
{
    public class PatientDetailsDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientPhoneNumber { get; set; } = string.Empty;
        public string? PatientEmail { get; set; }
        public MaritalStatus? PatientMaritalStatus { get; set; }
        public Gender PatientGender { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public List<SummarizedAppointmentDetailsDTO> Appointments { get; set; } = [];
    }
}
