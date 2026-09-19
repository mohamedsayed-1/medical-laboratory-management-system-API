using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Models.Enums;
using Medical_Laboratory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestedLabTestController : ControllerBase
    {
        private readonly IRequestedLabTestServices requestedLabTestServices;

        public RequestedLabTestController(IRequestedLabTestServices requestedLabTestServices)
        {
            this.requestedLabTestServices = requestedLabTestServices;
        }
        [HttpGet]
        public IActionResult GetAll(int pageNum, int pageSize)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            var requestedLabTest = requestedLabTestServices.GetAll(pageNum, pageSize);
            return Ok(requestedLabTest);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var requestedLabTest = requestedLabTestServices.GetById(id);
            if (requestedLabTest == null)
                return NotFound();
            return Ok(requestedLabTest);
        }

        [HttpGet("status")]
        public IActionResult GetAllByStatus(int pageNum, int pageSize, string status)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            if (status.ToLower() == "deleted" || Enum.TryParse<RequestedLabTestStatus>(status, ignoreCase: true, out _))
            {
                var requestedLabTests = requestedLabTestServices.GetAllByStatus(pageNum, pageSize, status);
                return Ok(requestedLabTests);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var requestedLabTestId = requestedLabTestServices.Delete(id);
            if (requestedLabTestId == null)
                return NotFound();
            return NoContent();
        }
    }
}
