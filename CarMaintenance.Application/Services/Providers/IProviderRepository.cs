using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Providers
{
    public interface IProviderRepository 
    {
        Task<Result<ProviderResponse>> AddAsync(ProviderRequest request);
        Task<Result<ProviderResponse>> UpdateAsync(int id ,ProviderRequest request);
        Task<Result<IEnumerable<ProviderResponse>>> GetAllActivatedVerifiedAsync();
        Task<Result<IEnumerable<ProviderResponse>>> GetAllAsync();
        Task<Result<ProviderResponse>> GetByIdAsync(int id);
        Task<Result>  DeleteAsync(int id);
        Task<Result> ToggleStatus(int id, ProviderRequestStatus ProviderRequestStatus);
    }
}
