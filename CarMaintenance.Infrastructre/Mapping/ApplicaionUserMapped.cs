using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Infrastructre.Identity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CarMaintenance.Infrastructre.Mapping
{
    public class ApplicaionUserMapped : Profile
    {
        
            public ApplicaionUserMapped()
            {

            CreateMap<UserRequest, ApplicationUser>().
                ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            CreateMap<ApplicationUser, UserResponse>();
            CreateMap<RegisterRequest, ApplicationUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<(ApplicationUser User, string Token, int ExpiresIn), RegisterResponse>()
                .ConstructUsing(src => new RegisterResponse
                {
                    Id = src.User.Id,
                    Email = src.User.Email!,
                    FullName = src.User.FullName,
                    token = src.Token,
                    ExpiresIn = src.ExpiresIn
                    
                });
            CreateMap<(ApplicationUser User, string Token, int ExpiresIn,string refreshToken , DateTime RefreshTokenExpiryDays), SignInResponse>().ConstructUsing(src => new SignInResponse
            {
                
                Email = src.User.Email!,
                FullName = src.User.FullName,
                token = src.Token,
                ExpiresIn = src.ExpiresIn,
                refreshToken = src.refreshToken,
                RefreshTokenExpiryDays = src.RefreshTokenExpiryDays

            });
            CreateMap<ApplicationUser, AccountResponse>();
            CreateMap<UpdateMyProfileRequest, ApplicationUser>();

        }
        
    }
}
