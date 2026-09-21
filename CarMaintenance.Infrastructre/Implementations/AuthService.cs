using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.UserErrorPrvider;
using CarMaintenance.Application.Services.Auth;
using CarMaintenance.Infrastructre.Identity;
using Microsoft.AspNetCore.Identity;



namespace CarMaintenance.Infrastructre.Implementations
{
    public class AuthService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager , IMapper mapper) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SignInResponse>> LogInAsync(SignInRequest request)
        {
            var isEmailExsited = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExsited is  null)
                return Result<SignInResponse>.Failure(UserError.NotFoundUser);

            var result  = await _signInManager.CheckPasswordSignInAsync(isEmailExsited, request.Password,false);
            if (result.Succeeded)
            {
                var response = _mapper.Map<SignInResponse>(isEmailExsited);
                return Result<SignInResponse>.Success(response);

            }
            
            return Result<SignInResponse>.Failure(UserError.InvaidCredentails);

        }

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            var isEmailExsited = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExsited is not null )
                return Result<RegisterResponse>.Failure(UserError.DuplicatedUser);
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {

                var response = _mapper.Map<RegisterResponse>(user);
                return Result<RegisterResponse>.Success(response);
            }

            var error = result.Errors.First();
            return Result<RegisterResponse>.Failure(new Error(error.Code, error.Description));
            
        }
    }
}
