using System.ComponentModel.DataAnnotations;
using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.DTOs
{
    public class AddAppointmentDTO
    {
        // Appointment information
        [Required]
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        
        // Patient information
        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Patient Date Of Birth")]
        public DateOnly PatientDateOfBirth { get; set; }

        [Required]
        [Display(Name = "Patient Gender")]
        public Gender PatientGender {  get; set; }
        
        [Required]
        [Display(Name = "Patient Phone Number")]
        [Length(11,11)]
        public string PatientPhoneNumber { get; set; } = string.Empty;
        
        [EmailAddress]
        [Display(Name = "Patient Email")]
        public string? PatientEmail { get; set; }
        
        [Display(Name = "Patient Marital Status")]
        public MaritalStatus? PatientMaritalStatus { get; set; } 

        [Required]
        [MinLength(1)]
        [Display(Name = "Lab Tests")]
        public List<int> LabTestsIds { get; set; } = [];
    }
}
