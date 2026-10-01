using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class ProviderReviewResponse
    {
        public int ProviderId { get; set; }
        public IEnumerable<ReviewResponse> ProductResonses { get; set; } = [];
    }
}
