using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;

namespace CarMaintenance.Infrastructre.Mapping
{
    public class ReviewMapped : Profile
    {
        public ReviewMapped()
        {
            CreateMap<ReviewRequest, Review>();
            CreateMap<Review, ReviewResponse>();
        }
    }
}
