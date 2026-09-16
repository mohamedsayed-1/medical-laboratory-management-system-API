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
            if (!appointmentServices.Add(appointmentDTO))
                return BadRequest();
            return Created();
        }
    }
}
