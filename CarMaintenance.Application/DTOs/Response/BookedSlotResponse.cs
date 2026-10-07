using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class BookedSlotResponse
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
