using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class RegistrationRequestDto
    {
        [Required]

        public DateTime AppointmentTime { get; set; }
    }
}
