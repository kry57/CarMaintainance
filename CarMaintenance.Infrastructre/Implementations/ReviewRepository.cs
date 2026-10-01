using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.DbErrorProvider;
using CarMaintenance.Application.ErrorProvider.ProductErrorProvider;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.ErrorProvider.ReviewErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Reviews;
using CarMaintenance.Infrastructre.Context;
using Humanizer;


namespace CarMaintenance.Infrastructre.Implementations
{
    public class ReviewRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : IReviewRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<ReviewResponse>> AddAsync(int providerId, ReviewRequest request)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result<ReviewResponse>.Failure(ReviewError.NotAuthorized);


            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == providerId && !p.IsDeleted);
            if (provider is null)
                return Result<ReviewResponse>.Failure(ProviderError.NotFound);
            if (customerId == provider.OwnerId)
                return Result<ReviewResponse>.Failure(ReviewError.CannotReviewOwnProvider);


            var existingReview = await _context.Reviews.AnyAsync(r => r.ProviderId == providerId && r.CustomerId == customerId && r.Comment.Trim().ToLower().Replace(" ", "") == request.Comment.Trim().ToLower().Replace(" ", "")
            && r.Rating == request.Rating && !r.IsDeleted);

            if (existingReview)
                return Result<ReviewResponse>.Failure(ReviewError.Duplicated);
            var review = _mapper.Map<Review>(request);
            review.ProviderId = providerId;
            review.CustomerId = customerId;

            await _context.Reviews.AddAsync(review);

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result<ReviewResponse>.Failure(ReviewError.NotAdded);

            var response = _mapper.Map<ReviewResponse>(review);

            return Result<ReviewResponse>.Success(response);



        }

        public async  Task<Result<IEnumerable<ProviderRateResponse>>> AverageReview(int? providerId)
        {
            var check = await _context.Products.AnyAsync(p => (providerId == null || p.ProviderId == providerId) && !p.IsDeleted && p.IsActive);
            if (!check)
                return Result<IEnumerable<ProviderRateResponse>>.Failure(ProductError.NotFound);
            var reviews = await (from r in _context.Reviews
                                 where !r.IsDeleted
                                 group r by r.ProviderId into groups
                                 select new ProviderRateResponse
                                 {
                                     ProviderId = groups.Key,
                                     AvgRate = groups.Average(g => g.Rating),
                                    ReviewerCount = groups.Count()
                                 }).AsNoTracking().ToListAsync();


            if (!reviews.Any())
                return Result<IEnumerable<ProviderRateResponse>>.Failure(ProductError.NotFound);

            return Result<IEnumerable<ProviderRateResponse>>.Success(reviews);
        }

        public async Task<Result> DeleteAsync(int providerId, int reviewId)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result.Failure(ReviewError.NotAuthorized);
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == providerId && !p.IsDeleted);
            if (provider is null)
                return Result.Failure(ProviderError.NotFound);

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ProviderId == providerId && r.Id == reviewId);
            if (review is null || review.IsDeleted)
                return Result.Failure(ReviewError.NotFound);

            review.IsDeleted = true;
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
                return Result.Failure(DbError.NotRemoved);

            return Result.Success();

        }

        public async Task<Result<IEnumerable<ProviderReviewResponse>>> GetAllAsync(int? providerId)
        {
            var check = await _context.Products.AnyAsync(p => (providerId == null || p.ProviderId == providerId) && !p.IsDeleted && p.IsActive);
            if (!check)
                return Result<IEnumerable<ProviderReviewResponse>>.Failure(ProductError.NotFound);
            var reviews = await (from r in _context.Reviews
                                 where !r.IsDeleted
                                 group r by r.ProviderId  into groups
                                 select new ProviderReviewResponse
                                 {
                                     ProviderId = groups.Key,
                                     ProductResonses = groups.Select(r => new ReviewResponse
                                     {
                                         ProviderId = r.ProviderId,
                                         Comment = r.Comment,
                                         CustomerId = r.CustomerId,
                                         IsDeleted = r.IsDeleted,
                                         Id = r.Id,
                                         Rating = r.Rating
                                     })
                                 }).AsNoTracking().ToListAsync();


            if (!reviews.Any())
                return Result<IEnumerable<ProviderReviewResponse>>
                    .Failure(ProductError.NotFound);

            return Result<IEnumerable<ProviderReviewResponse>>
                .Success(reviews);



        }

        public async  Task<Result<ReviewResponse>> GetByIdAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review is null)
                return Result<ReviewResponse>.Failure(ReviewError.NotFound);
            if (review.IsDeleted)
                return Result<ReviewResponse>.Failure(ReviewError.NotFound);
            var response = _mapper.Map<ReviewResponse>(review);
            return Result<ReviewResponse>.Success(response);
        }

        public async Task<Result> ToggleStatusAsync(int providerId, int reviewId)
        {
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == providerId && !p.IsDeleted);
            if (provider is null)
                return Result.Failure(ProviderError.NotFound);

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ProviderId == providerId && r.Id == reviewId);
            if (review is null)
                return Result.Failure(ReviewError.NotFound);
            review.IsDeleted = !review.IsDeleted;
            var rows  = await _context.SaveChangesAsync();
            if (rows <= 0)
                return Result.Failure(ReviewError.NotUpdated);
            return Result.Success();

        }

        public async Task<Result> UpdateAsync(int providerId, int reviewId, ReviewRequest request)
        {
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == providerId && !p.IsDeleted);
            if (provider is null)
                return Result.Failure(ProviderError.NotFound);

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ProviderId == providerId && r.Id == reviewId);
            if (review is null || review.IsDeleted)
                return Result.Failure(ReviewError.NotFound);
            _mapper.Map(request, review);
            var rows = await _context.SaveChangesAsync();

            if (rows <= 0)
                return Result.Failure(ReviewError.NotUpdated);
            return Result.Success();
        }
    }
}
