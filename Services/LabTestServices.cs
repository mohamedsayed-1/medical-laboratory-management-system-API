using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class LabTestServices : ILabTestServices
    {
        private readonly MLMSDbContext context;

        public LabTestServices(MLMSDbContext context)
        {
            this.context = context;
        }
        public List<LabTest> GetAllWithIds(List<int> ids)
        {
            return context.LabTests.Where(x => ids.Contains(x.Id)).ToList();
        }
    }
}
