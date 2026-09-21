using CarMaintenance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
        public UserRole UserRole = UserRole.Customer;
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
