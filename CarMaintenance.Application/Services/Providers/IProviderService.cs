using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Domain.Entities;
using CarMaintenance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Providers
{
    public interface IProviderService
    {
        Task<Result> ToggleStatus(int id,bool? activated, bool? verified, bool? deleted);
        Task<Result<IEnumerable<ProviderResponse>>> GetProvidersByOwner(string id );
        Task<Result<IEnumerable<OwnerResponse>>> GetProvidersGroupedByOwner();
        Task<Result<IEnumerable<ProviderResponse>>> GetNearbyProviders(double lat, double lng, double radiusKm, ProviderCategory? category = null);
    }
}
