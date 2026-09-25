using AutoMapper;
using CarMaintenance.Domain.Entities;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;

namespace CarMaintenance.Application.Mapping
{
    public class ProductImageMapped : Profile
    {
        public ProductImageMapped()
        {
            CreateMap<ProductImageRequest, ProductImage>();
            CreateMap<ProductImage, ProductImageResponse>();
        }
    }
}