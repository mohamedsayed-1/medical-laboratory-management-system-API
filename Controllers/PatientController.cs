using System.Drawing.Printing;
using Medical_Laboratory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientServices patientServices;

        public PatientController(IPatientServices patientServices)
        {
            this.patientServices = patientServices;
        }

        [HttpGet]
        public IActionResult GetAll(int pageNum, int pageSize)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            var patients = patientServices.GetAll(pageNum, pageSize);
            return Ok(patients);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var patient = patientServices.GetById(id);
            if(patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpGet("/phoneNumber/{phoneNumber}")]
        public IActionResult GetByPhoneNumber(string phoneNumber)
        {
            var patient = patientServices.GetByPhoneNumber(phoneNumber);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }
    }
}
