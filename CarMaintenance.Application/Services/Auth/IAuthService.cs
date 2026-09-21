using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request);
        Task<Result<SignInResponse>> LogInAsync(SignInRequest request);
    }
}
