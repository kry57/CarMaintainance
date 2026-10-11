using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.UserErrorPrvider;
using CarMaintenance.Application.Services.User;
using Microsoft.AspNetCore.Identity;
using CarMaintenance.Infrastructre.Identity;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class UserService(UserManager<ApplicationUser> userManager , IMapper mapper) : 
        IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserResponse>> AddAsync(UserRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                return Result<UserResponse>.Failure(UserError.DuplicatedUser);

            var user = _mapper.Map<ApplicationUser>(request);
            var respondedUser = _mapper.Map<UserResponse>(user);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                return Result<UserResponse>.Success(respondedUser);

            var error = result.Errors.First();
            return Result<UserResponse>.Failure(new Error(error.Code, error.Description));
        }

        public async Task<Result<IEnumerable<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users
                .AsNoTracking()
                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<IEnumerable<UserResponse>>.Success(users);
        }

        public async Task<Result<UserResponse>> GetByIdAsync(string userId)
        {
            var user = await _userManager.Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return user is null
                ? Result<UserResponse>.Failure(UserError.NotFoundUser)
                : Result<UserResponse>.Success(user);
        }

        public async Task<Result> Unlock(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserError.NotFoundUser);
            if (!user.LockoutEnabled)
                return Result.Failure(UserError.UnEnabled);
            if (user.LockoutEnd < DateTime.UtcNow || user.LockoutEnd is null)
                return Result.Failure(UserError.Free);
            var result = await _userManager.SetLockoutEndDateAsync(user, null);
            if (result.Succeeded)
                return Result.Failure(UserError.NotUnlocked);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(string userId, UpdateMyProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure(UserError.NotFoundUser);

            user.FullName = request.FullName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description));
        }
    }
}
