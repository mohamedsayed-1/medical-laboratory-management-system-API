using System.Linq.Expressions;
using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class RequestedLabTestServices : IRequestedLabTestServices
    {
        private readonly MLMSDbContext context;

        public RequestedLabTestServices(MLMSDbContext context)
        {
            this.context = context;
        }
        public List<RequestedLabTestDetailsDTO> GetAll(int pageNum, int pageSize)
        {
            var requestedLabTests = context.RequestedLabTests
                .Select(ToRequestedLabTestsDetailsDTO())
                .OrderBy(x => x.RequestedLabTestId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return requestedLabTests;
        }

        private Expression<Func<RequestedLabTest, RequestedLabTestDetailsDTO>> ToRequestedLabTestsDetailsDTO()
        {
            return x => new RequestedLabTestDetailsDTO()
            {
                LabTestName = x.LabTest.Name,
                LabTestPrice = x.LabTest.Price,
                RequestedLabTestId = x.Id,
                RequestedLabTestStatus = x.Status,
                LabTestResultNotes = x.LabTestResult != null ? x.LabTestResult.Notes : null,
                LabTestResultValue = x.LabTestResult != null ? x.LabTestResult.Value : null
            };
        }
    }
}
