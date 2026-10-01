namespace Medical_Laboratory_Management_System.DTOs
{
    public class RegisterDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Role { get; set; }
    }
}
