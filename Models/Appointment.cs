using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        public AppointmentStatus Status { get; private set; }
        public bool IsDeleted { get; set; } = false;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public ICollection<RequestedLabTest> RequestedLabTests { get; set; } = [];
        public static Appointment Create(Patient patient, ICollection<RequestedLabTest> requestedLabTests)
        {
            return new Appointment()
            {
                Status = AppointmentStatus.Scheduled,
                Patient = patient,
                RequestedLabTests = requestedLabTests
            };
        }
        public void Cancel()
        {
            Status = AppointmentStatus.Cancelled;
        }
        public void Process()
        {
            Status = AppointmentStatus.Processing;
        }
        public void Complete()
        {
            Status = AppointmentStatus.Completed;
        }
    }
}