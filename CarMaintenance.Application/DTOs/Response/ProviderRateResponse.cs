using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class ProviderRateResponse
    {
        public int ProviderId { get; set; }
        public double AvgRate { get; set; }
        public int  ReviewerCount { get; set; }
    }
}
