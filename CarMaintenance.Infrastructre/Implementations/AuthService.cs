using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.UserErrorPrvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Auth;
using CarMaintenance.Infrastructre.Context;
using CarMaintenance.Infrastructre.Identity;
using CarMaintenance.Infrastructre.JWT;
using CarMaintenance.Infrastructre.Seeding;
using CarMaintenance.Infrastructre.Temp;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;



namespace CarMaintenance.Infrastructre.Implementations
{
    public class AuthService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager ,IJWTService jWTService, IMapper mapper , 
        ICurrentUserService currentUserService,
        ApplicationDbContext context, IEmailService emailService,IHttpContextAccessor httpContextAccessor,ILogger<AuthService> logger)
        : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJWTService _jWTService = jWTService;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ApplicationDbContext _context = context;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly int _refreshRokenExpiryDays = 14;

        public async Task<Result<SignInResponse>> LogInAsync(SignInRequest request)
        {
            var isEmailExsited = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExsited is  null)
                return Result<SignInResponse>.Failure(UserError.NotFoundUser);

            if (!isEmailExsited.EmailConfirmed)
                return Result<SignInResponse>.Failure(UserError.InvaidCredentails);



            var result  = await _signInManager.CheckPasswordSignInAsync(isEmailExsited, request.Password, true);
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(isEmailExsited);
                var roleClaims = await (
                    from r in _context.Roles
                    join p in _context.RoleClaims on r.Id equals p.RoleId
                    where userRoles.Contains(r.Name!) && p.ClaimType == Permissions.Type
                     select p.ClaimValue
                        ).Distinct().ToListAsync();


                var (token, expiresIn) = _jWTService.GenerateToken(isEmailExsited,userRoles, roleClaims!);
                var refreshToken = GenerateRefreshToken();

                isEmailExsited.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = DateTime.UtcNow.AddDays(_refreshRokenExpiryDays)
                });
                await _userManager.UpdateAsync(isEmailExsited);

                var response = _mapper.Map<SignInResponse>((isEmailExsited,token,expiresIn, refreshToken,DateTime.UtcNow.AddDays(_refreshRokenExpiryDays)));
                return Result<SignInResponse>.Success(response);

            }
            
            return Result<SignInResponse>.Failure(UserError.InvaidCredentails);

        }

        public async Task<Result> RegisterAsync(RegisterRequest request)
        {
            var isEmailExsited = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExsited is not null )
                return Result.Failure(UserError.DuplicatedUser);
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                
                // generate a code 
                var verifyCode =  await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodeVerifyCode = Encoding.UTF8.GetBytes(verifyCode);
                var encodeToText  = WebEncoders.Base64UrlEncode(encodeVerifyCode);
                //send a code 
                BackgroundJob.Enqueue(() => SendEmail(user, encodeToText));
                _logger.LogInformation("Code : {value}", encodeToText);

                
                return Result.Success();
            }

            var error = result.Errors.First();
            return Result<RegisterResponse>.Failure(new Error(error.Code, error.Description));
            
        }
        public async Task<Result<SignInResponse>> GenerateRefreshToken(string token, string refreshToken)
        {
            // validate token
            var UserId = _jWTService.ValidateToken(token);
            if (UserId == null)
                return Result<SignInResponse>.Failure(UserError.InvaidCredentails);
            //validate refreshToken 

            var user = await _userManager.FindByIdAsync(UserId);
            if(user is null)
                return Result<SignInResponse>.Failure(UserError.NotFoundUser);

            if (user.LockoutEnd > DateTime.UtcNow)
                return Result<SignInResponse>.Failure(UserError.LockedOut);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            if( userRefreshToken is null || !userRefreshToken.IsActive)
                return Result<SignInResponse>.Failure(UserError.NotFoundUser);

            var userRoles = await _userManager.GetRolesAsync(user);
            var roleClaims = await _context.Roles.Join(
                _context.RoleClaims, role => role.Id, claim => claim.RoleId, (role, Claim) => new { role, Claim }).
                Where(a => userRoles.Contains(a.role.Name!)).Select(a => a.Claim.ClaimValue).
              ToListAsync();


            var (newToken, expiresIn) = _jWTService.GenerateToken(user, userRoles, roleClaims!);

            var newRefreshToken = GenerateRefreshToken();


            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshRokenExpiryDays)
            });

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);



            var response = _mapper.Map<SignInResponse>((user, newToken, expiresIn, newRefreshToken, DateTime.UtcNow.AddDays(_refreshRokenExpiryDays)));
            return Result<SignInResponse>.Success(response);
        }
        public async Task<Result> GetRevoked(string token, string refreshToken)
        { 
           var UserId = _jWTService.ValidateToken(token);
            if (UserId == null)
                return Result<SignInResponse>.Failure(UserError.InvaidCredentails);
            //validate refreshToken 

            var user = await _userManager.FindByIdAsync(UserId);
            if(user is null)
                return Result<SignInResponse>.Failure(UserError.NotFoundUser);

            if (user.LockoutEnd > DateTime.UtcNow)
                return Result<SignInResponse>.Failure(UserError.LockedOut);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            if(userRefreshToken is null || !userRefreshToken.IsActive)
                return Result<SignInResponse>.Failure(UserError.NotFoundUser);
            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            
            return Result.Success();
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        public  async Task SendEmail(ApplicationUser user , string code )
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            var body = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
         new Dictionary<string, string>
         {
                       { "{{name}}" ,$"{user.FullName}" },
                       {"{{action_url}}",$"{origin}/auth/ConfirmEmail?userId={user.Id}&Code={code}"}
         }

          );
                await _emailService.SendAsync( user.Email!, "CarMaintenance : Email Confirmation", body);
        }
        public  async Task SendResetpasswordEmail(ApplicationUser user , string code )
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            var body = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
         new Dictionary<string, string>
         {
                       { "{{name}}" ,$"{user.FullName}" },
                       {"{{action_url}}",$"{origin}/auth/ConfirmEmail?userId={user.Id}&Code={code}"}
         }

          );
                await _emailService.SendAsync( user.Email!, "CarMaintenance : ForgetPasswordEmailCofirmation", body);
        }

        public async Task<Result> ConfirmEmail(string UserId, string Code)
        {
            // isUserIdExsited ? 
            var isUserIdExsited = await _userManager.FindByIdAsync(UserId);
            if (isUserIdExsited is null)
                return Result.Failure(UserError.NotFoundUser);
            if (isUserIdExsited.EmailConfirmed)
                return Result.Failure(UserError.DuplicatedConfirmation);
            Byte[] decodedBtyes;
            var text = string.Empty;
            try
            {
                var bytes = WebEncoders.Base64UrlDecode(Code);
                text  = Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                return Result.Failure(UserError.InvaidCredentails);
            }
            // check 
            var validCode = await _userManager.ConfirmEmailAsync(isUserIdExsited, text);
            if (validCode.Succeeded)
            {
                await _userManager.AddToRoleAsync(isUserIdExsited, DefaultRole.Member);
                return Result.Success();
            }
            var error = validCode.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description));

        }

        public async Task<Result> ResendEmailConfirmation(ResendEmailConfirmationRequest request)
        {
            var isEmailExsited = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExsited is null)
                return Result.Success(); // f nas bt3mel kda 3ashan misleading -_-
            if(isEmailExsited.EmailConfirmed)
                return Result.Failure(UserError.DuplicatedConfirmation);
            // generate the code 
            var verifyCode = await _userManager.GenerateEmailConfirmationTokenAsync(isEmailExsited);
            var encodeVerifyCode = Encoding.UTF8.GetBytes(verifyCode);
            var encodeToText = WebEncoders.Base64UrlEncode(encodeVerifyCode);
            //send a code 
            BackgroundJob.Enqueue(() => SendEmail(isEmailExsited, encodeToText));
            _logger.LogInformation("Code : {value}", encodeToText);
            return Result.Success();
        }

        public async  Task<Result> ForgetPassword(ForgetPasswordRequest request)
        {
            var isEmailExsited = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExsited is null)
                return Result.Success(); // f nas bt3mel kda 3ashan misleading -_-
            if(!isEmailExsited.EmailConfirmed)
                return Result.Failure(UserError.ConfirmEmailFrist);
            // how can i send the code ?? 
            var decodedCode = await _userManager.GeneratePasswordResetTokenAsync(isEmailExsited);
            var bytes = Encoding.UTF8.GetBytes(decodedCode);
            var encodedCode = WebEncoders.Base64UrlEncode(bytes);

            _logger.LogInformation("code : {code}", encodedCode);
            BackgroundJob.Enqueue(() => SendResetpasswordEmail(isEmailExsited, encodedCode));
            return Result.Success();
        }

        public async Task<Result> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            
            if (user is null)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserError.ConfirmEmailFrist);

            string code;
            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            }
            catch (FormatException)
            {
                return Result.Failure(UserError.InvaidCredentails);
            }

            var result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description));
        }
    }
}
