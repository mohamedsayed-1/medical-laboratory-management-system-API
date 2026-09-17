using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.DTOs
{
    public class RequestedLabTestDetailsDTO
    {
        public int RequestedLabTestId { get; set; }
        public RequestedLabTestStatus RequestedLabTestStatus { get; set; }
        public string? LabTestResultValue { get; set; }
        public string? LabTestResultNotes { get; set; }
        public string LabTestName { get; set; } = string.Empty;
        public decimal LabTestPrice { get; set; }
    }
}
