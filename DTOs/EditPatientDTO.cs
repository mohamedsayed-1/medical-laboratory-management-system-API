using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.DTOs
{
    public class EditPatientDTO
    {
        public string? Email { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}
