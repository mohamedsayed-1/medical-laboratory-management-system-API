using Medical_Laboratory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestServices labTestServices;

        public LabTestController(ILabTestServices labTestServices)
        {
            this.labTestServices = labTestServices;
        }

        [HttpGet]
        public IActionResult GetAll(int pageNum, int pageSize)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            var labTests = labTestServices.GetAll();
            return Ok(labTests);
        }
    }
}
