using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
