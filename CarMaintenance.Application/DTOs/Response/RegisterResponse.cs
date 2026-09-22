using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class RegisterResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string token { get; set; }
        public int ExpiresIn { get; set; }
        

    }
}
