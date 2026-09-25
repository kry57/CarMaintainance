using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class ProviderProductResponse
    {
        public int ProviderId { get; set; }
        public IEnumerable<ProductResonse> ProductResonses { get; set; } = [];
    }
}
