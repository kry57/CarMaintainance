using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class ProviderResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public double Latitude { get; set; }


        public double Longitude { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
