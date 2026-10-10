using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class AccountResponse
    {
        public string Email { get; set; }    = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
