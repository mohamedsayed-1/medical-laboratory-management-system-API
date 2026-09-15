namespace Medical_Laboratory_Management_System.Models
{
    public class LabTest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<RequestedLabTest> RequestedLabTests { get; set; } = [];
    }
}