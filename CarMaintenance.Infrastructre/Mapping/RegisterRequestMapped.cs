using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Infrastructre.Mapping
{
    public class RegisterRequestMapped : Profile
    {
        public RegisterRequestMapped()
        {
            //CreateMap<RegistrationRequestDto, RegisterRequest>();
            CreateMap<RegistrationRequest, RegistrationRequestResponse>();
        }
    }
}
