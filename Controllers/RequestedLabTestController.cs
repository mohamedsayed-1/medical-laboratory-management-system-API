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

    }
}
