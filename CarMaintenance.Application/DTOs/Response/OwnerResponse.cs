using CarMaintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class OwnerResponse
    {
        public string OwnerId { get; set; }
        public ICollection<ProviderResponse> Providers { get; set; }
    }
}
