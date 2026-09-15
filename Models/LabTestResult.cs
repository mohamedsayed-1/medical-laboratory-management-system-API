namespace Medical_Laboratory_Management_System.Models
{
    public class LabTestResult
    {
        public int Id { get; set; }
        public required string Value {  get; set; }
        public string? Notes { get; set; }
        public int RequestedLabTestId { get; set; }
        public required RequestedLabTest RequestedLabTest { get; set; }
    }
}