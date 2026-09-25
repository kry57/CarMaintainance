using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Infrastructre.Mapping
{
    public class ProductMapped : Profile
    {
        public ProductMapped()
        {
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<Product, ProductResonse>().ReverseMap();
            //CreateMap<Provider, ProviderResponse>().ReverseMap();
        }
    }
}
