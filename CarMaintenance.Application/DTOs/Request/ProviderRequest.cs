using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ProviderRequest
    {
        [Required,StringLength(30 , MinimumLength =10)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(256, MinimumLength = 50)]
        public string Description { get; set; } = string.Empty;
        [Required, StringLength(50, MinimumLength = 10)]
        public string Address { get; set; } = string.Empty;
        [Required, StringLength(15,MinimumLength =5)]
        public string City { get; set; } = string.Empty;
        [Required, Range(-90, 90)]
        public double Latitude { get; set; }

        [Required, Range(-180, 180)]
        public double Longitude { get; set; }
        [Required,Phone,StringLength(20,MinimumLength =11)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
