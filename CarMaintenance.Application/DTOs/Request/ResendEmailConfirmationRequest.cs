using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ResendEmailConfirmationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; 
    }
}
