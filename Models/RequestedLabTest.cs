using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Models
{
    public class RequestedLabTest
    {
        public int Id { get; set; }
        public RequestedLabTestStatus Status { get; private set; }
        public bool IsDeleted { get; set; } = false;
        public LabTestResult? LabTestResult { get; private set; }
        public int AppointmentId { get; set; }
        public required Appointment Appointment { get; set; }
        public int LabTestId { get; set; }
        public required LabTest LabTest { get; set; }

        public static RequestedLabTest Create(Appointment appointment, LabTest labTest)
        {
            return new RequestedLabTest()
            {
                Status = RequestedLabTestStatus.Queued,
                Appointment = appointment,
                LabTest = labTest
            };
        }
        public void Cancel()
        {
            Status = RequestedLabTestStatus.Cancelled;
        }
        public void Process()
        {
            Status = RequestedLabTestStatus.Processing;
        }

        public void CompleteWithResult(LabTestResult result)
        {
            LabTestResult = result;
            Status = RequestedLabTestStatus.Completed;
        }
    }
}