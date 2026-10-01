using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Infrastructre.Mapping
{
    public class SubscriptionMapped : Profile
    {
        public SubscriptionMapped()
        {
            CreateMap<SubscriptionRequest, Subscription>();
            CreateMap<Subscription, SubscriptionResponse>();
        }
    }
}
