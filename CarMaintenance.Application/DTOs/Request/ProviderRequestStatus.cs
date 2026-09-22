using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ProviderRequestStatus
    {
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsVerified { get; set; }
    }
}
