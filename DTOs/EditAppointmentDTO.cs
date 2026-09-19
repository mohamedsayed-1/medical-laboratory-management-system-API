namespace Medical_Laboratory_Management_System.DTOs
{
    public class EditAppointmentDTO
    {
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public bool? Urgent { get; set; }

    }
}
