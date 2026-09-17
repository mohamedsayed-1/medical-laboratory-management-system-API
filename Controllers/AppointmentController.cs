using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentServices appointmentServices;

        public AppointmentController(IAppointmentServices appointmentServices)
        {
            this.appointmentServices = appointmentServices;
        }
        [HttpPost]
        public IActionResult Add(AddAppointmentDTO appointmentDTO)
        {
            var appointment = appointmentServices.Add(appointmentDTO);
            if(appointment is null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointmentServices.GetByIdWithIncludes(appointment.Id));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var appointment = appointmentServices.GetByIdWithIncludes(id);
            if(appointment is null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNum = 1, int pageSize = 10)
        {
            if (pageSize <= 0 || pageNum <= 0)
                return BadRequest();
            var appointments = appointmentServices.GetAllWithIncludes(pageNum, pageSize);
            return Ok(appointments);
        }
    }
}
