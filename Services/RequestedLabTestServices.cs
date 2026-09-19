using System.Linq.Expressions;
using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Exceptions;
using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System.Services
{
    public class RequestedLabTestServices : IRequestedLabTestServices
    {
        private readonly MLMSDbContext context;

        public RequestedLabTestServices(MLMSDbContext context)
        {
            this.context = context;
        }

        public int? Cancel(int id)
        {
            var requestedLabTest = context.RequestedLabTests
               .Where(x => x.Id == id).FirstOrDefault();
            if (requestedLabTest == null)
                return null;
            if (requestedLabTest.Status != Models.Enums.RequestedLabTestStatus.Queued)
                throw new RequestedLabTestNotCancellableException($"Requested Lab Test {id} cannot be cancelled because it's status is \"{requestedLabTest.Status}\"");
            requestedLabTest.Cancel();
            context.SaveChanges();
            return requestedLabTest.Id;

        }

        public int? Delete(int id)
        {
            var requestedLabTest = context.RequestedLabTests
                .Where(x => x.Id == id).FirstOrDefault();
            if (requestedLabTest == null)
                return null;
            requestedLabTest.IsDeleted = true;
            context.SaveChanges();
            return requestedLabTest.Id;
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

        public List<RequestedLabTestDetailsDTO>? GetAllByStatus(int pageNum, int pageSize, string status)
        {
            IQueryable<RequestedLabTest> requestedLabTests;
            if (status == null)
                return null;

            if (status.Equals("deleted", StringComparison.OrdinalIgnoreCase))
            {
                requestedLabTests = context.RequestedLabTests
                .IgnoreQueryFilters()
                .Where(x => x.IsDeleted);
            }
            else
            {
                requestedLabTests = context.RequestedLabTests
                .Where(x => x.Status.ToString().ToUpper() == status.ToUpper());
            }
            var requestedLabTestDTO = requestedLabTests
                .Select(ToRequestedLabTestsDetailsDTO())
                .OrderBy(x => x.RequestedLabTestId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return requestedLabTestDTO;
        }

        public RequestedLabTestDetailsDTO? GetById(int id)
        {
            return context.RequestedLabTests
                .Where(x => x.Id == id)
                .Select(ToRequestedLabTestsDetailsDTO())
                .FirstOrDefault();
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
