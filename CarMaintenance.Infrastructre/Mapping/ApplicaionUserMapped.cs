using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Infrastructre.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Mapping
{
    public class ApplicaionUserMapped : Profile
    {
        
            public ApplicaionUserMapped()
            {

            CreateMap<RegisterRequest, ApplicationUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, RegisterResponse>();
            CreateMap<ApplicationUser, SignInResponse>();

            }
        
    }
}
