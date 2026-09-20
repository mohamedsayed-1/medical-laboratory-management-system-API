using Medical_Laboratory_Management_System.Exceptions;
using Medical_Laboratory_Management_System.DTOs;
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

        [HttpPost("{id:int}/cancel")]
        public IActionResult Cancel(int id)
        {
            try
            {
                var requestedLabTestId = requestedLabTestServices.Cancel(id);
                if (requestedLabTestId == null)
                    return NotFound();
                return Ok(requestedLabTestServices.GetById(requestedLabTestId.Value));
            }
            catch (RequestedLabTestNotCancellableException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("process")]
        public IActionResult StartProcessing(int id)
        {
            try
            {
                var requestedLabTestId = requestedLabTestServices.StartProcessing(id);
                if (requestedLabTestId == null)
                    return NotFound();
                return Ok(requestedLabTestServices.GetById(requestedLabTestId.Value));
            }
            catch(RequestedLabTestNotProcessableException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
