using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Infrastructre.Mapping
{
    public class ProviderMapped : Profile
    {
        public ProviderMapped()
        {
            CreateMap<ProviderRequest, Provider>().ReverseMap();
            CreateMap<Provider, ProviderRequestStatus>().ReverseMap();
            CreateMap<Provider, ProviderResponse>().ReverseMap();
        }
    }
}
