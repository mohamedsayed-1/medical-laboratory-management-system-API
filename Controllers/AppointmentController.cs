using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Models.Enums;
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
            if (appointment is null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointmentServices.GetByIdWithIncludes(appointment.Id));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var appointment = appointmentServices.GetByIdWithIncludes(id);
            if (appointment is null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNum, int pageSize)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            var appointments = appointmentServices.GetAllWithIncludes(pageNum, pageSize);
            return Ok(appointments);
        }
        [HttpGet("phoneNumber/{phoneNumber}")]
        public IActionResult GetAllByPhoneNumber(int pageNum, int pageSize, string? status, string phoneNumber)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            if (status == "Deleted" || Enum.TryParse<AppointmentStatus>(status, ignoreCase: true, out _))
            {
                var appointments = appointmentServices.GetAllByPhoneNumberWithIncludes(pageNum, pageSize, status, phoneNumber);
                return Ok(appointments);
            }
            return BadRequest();
        }
        [HttpGet("status")]
        public IActionResult GetAllByStatus(int pageNum, int pageSize, string status)
        {
            if (pageSize <= 0 || pageNum <= 0)
            {
                pageSize = 10;
                pageNum = 1;
            }
            if (status == "Deleted" || Enum.TryParse<AppointmentStatus>(status, ignoreCase:true,out _))  
            {
                var appointments = appointmentServices.GetAllWithIncludesFilterByStatus(pageNum, pageSize, status);
                return Ok(appointments);
            }
            return BadRequest();
        }
    }
}
