using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Application.Services.Subscriptions
{
    public interface ISubscriptionsRepository
    {
        Task<Result<SubscriptionResponse>> AddAsync(SubscriptionRequest request);
        Task<Result> CancelAsync(int subscriptionId);
        Task<Result<SubscriptionResponse>> GetCurrentAsync();
        Task<Result<IEnumerable<SubscriptionResponse>>> GetHistoryAsync();
        Task<Result<SubscriptionResponse>> RenewAsync();
        Task<Result<SubscriptionResponse>> UpgradeAsync(SubscriptionRequest request);
        Task<Result> DowngradeAsync(SubscriptionRequest request);
        //Task<Result> DeleteAsync(int providerId, int reviewId);
        //Task<Result> UpdateAsync(int providerId, int reviewId, ReviewRequest request);
        //Task<Result> ToggleStatusAsync(int providerId, int reviewId);
        //Task<Result<IEnumerable<ProviderReviewResponse>>> GetAllAsync(int? providerId);
        //Task<Result<ReviewResponse>> GetByIdAsync(int reviewId);
        //Task<Result<IEnumerable<ProviderRateResponse>>> AverageReview(int? providerId);
    }
}
