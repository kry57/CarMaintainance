using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;

namespace CarMaintenance.Application.Services.User
{
    public interface IUserService 
    {
        Task<Result<UserResponse>> AddAsync(UserRequest request);
        Task<Result<UserResponse>> GetByIdAsync(string  userId);
        Task<Result<IEnumerable<UserResponse>>> GetAllAsync();
        Task<Result> UpdateAsync(string userId, UpdateMyProfileRequest request);
        Task<Result> Unlock(string id);
    }
}
