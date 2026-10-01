using AutoMapper.Configuration.Conventions;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;

namespace CarMaintenance.Application.Services.Reviews
{
    public interface IReviewRepository
    {
        Task<Result<ReviewResponse>> AddAsync(int providerId, ReviewRequest request);
        Task<Result> DeleteAsync(int providerId, int reviewId);
        Task<Result> UpdateAsync(int providerId, int reviewId, ReviewRequest request);
        Task<Result> ToggleStatusAsync(int providerId, int reviewId);
        Task<Result<IEnumerable<ProviderReviewResponse>>> GetAllAsync(int? providerId);
        Task<Result<ReviewResponse>> GetByIdAsync(int reviewId);
        Task<Result<IEnumerable<ProviderRateResponse>>> AverageReview(int? providerId);

    }
}