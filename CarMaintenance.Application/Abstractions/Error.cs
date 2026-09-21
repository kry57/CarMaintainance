using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Abstractions
{
    public record Error(string Code , string Message )
    {
        public static readonly Error None = new("","");
    }
}
