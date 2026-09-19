using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Exceptions;
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

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var labTest = labTestServices.GetById(id);
            if (labTest == null)
                return NotFound();
            return Ok(labTest);
        }

        [HttpPost]
        public IActionResult Add(AddLabTestDTO labTestDTO)
        {
            var labTestId = labTestServices.Add(labTestDTO);
            return CreatedAtAction(nameof(GetById), new { id = labTestId }, labTestServices.GetById(labTestId));
        }

        [HttpPatch("{id:int}")]
        public IActionResult Edit(int id, EditLabTestDTO labTestDTO)
        {
            var labTestId = labTestServices.Edit(id, labTestDTO);
            if (labTestId == null)
                return NotFound();
            return Ok(labTestServices.GetById(labTestId.Value));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var labTestId = labTestServices.Delete(id);
            if (labTestId == null)
                return NotFound();
            return NoContent();
        }
    }
}