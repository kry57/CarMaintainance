using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Domain.Enums;

namespace CarMaintenance.Application.Services.RegistrationsRequests
{
    public interface IRegistrationRequestsRepository
    {
        Task<Result<RegistrationRequestResponse>> AddAsync(string customerId, int providerId, RegistrationRequestDto request);
        Task<Result<RegistrationRequestResponse>> GetStatusAsync(string callerId, int requestId);
        Task<Result> CancelAsync(string callerId, int requestId);
        Task<Result<CustomerRegistrationRequestResponse>> GetMyRequestsAsync(string callerId);
        Task<Result<IEnumerable<CustomerRegistrationRequestResponse>>> GetProviderRequestsAsync(string callerId, int providerId, RequestStatus? status);
        Task<Result<IEnumerable<BookedSlotResponse>>> GetBookedSlotsAsync(int providerId, DateOnly date);
        //Task<Result> DeleteAsync(int providerId, int reviewId);
        //Task<Result> UpdateAsync(int providerId, int reviewId, ReviewRequest request);
        //Task<Result> ToggleStatusAsync(int providerId, int reviewId);
        //Task<Result<IEnumerable<ProviderReviewResponse>>> GetAllAsync(int? providerId);
        //Task<Result<ReviewResponse>> GetByIdAsync(int reviewId);
        //Task<Result<IEnumerable<ProviderRateResponse>>> AverageReview(int? providerId);
    }
}
