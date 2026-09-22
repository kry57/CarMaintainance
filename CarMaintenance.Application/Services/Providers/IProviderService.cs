using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Providers
{
    public interface IProviderService
    {
        Task<Result> ToggleStatus(int id,bool? activated, bool? verified, bool? deleted);
    }
}
