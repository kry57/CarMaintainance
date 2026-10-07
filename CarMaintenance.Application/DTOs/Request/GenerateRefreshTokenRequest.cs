using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class GenerateRefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
