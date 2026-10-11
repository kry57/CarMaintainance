using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class DiagnosisResponse
    {
        public string Answer { get; set; } = string.Empty;

        public string? MatchedFault { get; set; }

        public double MatchScore { get; set; }

        public bool IsVerified { get; set; }
    }

}
