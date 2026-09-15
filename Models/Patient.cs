using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender {  get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}