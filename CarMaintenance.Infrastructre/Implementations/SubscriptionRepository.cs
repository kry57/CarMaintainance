using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.ErrorProvider.SubscriptionErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Subscriptions;
using CarMaintenance.Domain.Enums;
using CarMaintenance.Infrastructre.Context;
using Microsoft.Data.SqlClient;


namespace CarMaintenance.Infrastructre.Implementations
{
    public class SubscriptionRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : ISubscriptionsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<SubscriptionResponse>> AddAsync(SubscriptionRequest request)
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result<SubscriptionResponse>.Failure(ProviderError.NotFound);

            var planName = request.PlanName.Trim().ToLowerInvariant().Replace(" ", "");

            if (!PlanPrices.Prices.ContainsKey(planName))
                return Result<SubscriptionResponse>.Failure(SubscriptionError.InvalidPlan);

            var hasActiveSubscription = await _context.Subscriptions.AnyAsync(s => s.ProviderId == provider.Id && s.SubscriptionStatus == SubscriptionStatus.Active && s.EndAt > DateTime.UtcNow);


            if (hasActiveSubscription)
                return Result<SubscriptionResponse>.Failure(SubscriptionError.AlreadySubscribed);

            var subscription = new Subscription
            {
                ProviderId = provider.Id,
                PlanName = planName,
                StartAt = DateTime.UtcNow,
                EndAt = DateTime.UtcNow.AddMonths(1),
                SubscriptionStatus = SubscriptionStatus.Active
            };

            await _context.Subscriptions.AddAsync(subscription);

            try
            {
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows <= 0)
                    return Result<SubscriptionResponse>.Failure(SubscriptionError.NotAdded);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
            {
                return Result<SubscriptionResponse>.Failure(SubscriptionError.AlreadySubscribed);
            }

            var response = _mapper.Map<SubscriptionResponse>(subscription);

            return Result<SubscriptionResponse>.Success(response);
        }

        public async Task<Result> CancelAsync(int subscriptionId)
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result.Failure(ProviderError.NotFound);

            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Id == subscriptionId && s.ProviderId == provider.Id);

            if (subscription is null)
                return Result.Failure(SubscriptionError.NotFound);

            if (subscription.SubscriptionStatus == SubscriptionStatus.Cancelled)
                return Result.Failure(SubscriptionError.AlreadyCancelled);

            if (subscription.PendingPlanName is not null)
            {
                subscription.PendingPlanName = null;
            }
            else
            {
                if(subscription.SubscriptionStatus == SubscriptionStatus.Upgraded)
                {
                    subscription.SubscriptionStatus = SubscriptionStatus.Active;
                }
                else
                {

                subscription.SubscriptionStatus = SubscriptionStatus.Cancelled;
                }
            }

            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0
                ? Result.Success()
                : Result.Failure(SubscriptionError.NotUpdated);
        }

        public async Task<Result<SubscriptionResponse>> GetCurrentAsync()
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result<SubscriptionResponse>.Failure(ProviderError.NotFound);

            var subscription = await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.ProviderId == provider.Id && s.SubscriptionStatus == SubscriptionStatus.Active && s.EndAt > DateTime.UtcNow)
                .OrderByDescending(s => s.EndAt)
                .FirstOrDefaultAsync();
            return subscription is null
        ? Result<SubscriptionResponse>.Failure(SubscriptionError.NotFound)
        : Result<SubscriptionResponse>.Success(_mapper.Map<SubscriptionResponse>(subscription));
        }

        public async Task<Result<IEnumerable<SubscriptionResponse>>> GetHistoryAsync()
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result<IEnumerable<SubscriptionResponse>>.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result<IEnumerable<SubscriptionResponse>>.Failure(ProviderError.NotFound);

            var subscriptions = await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.ProviderId == provider.Id)
                .OrderByDescending(s => s.StartAt)
                .ToListAsync();

            var response = _mapper.Map<IEnumerable<SubscriptionResponse>>(subscriptions);

            return Result<IEnumerable<SubscriptionResponse>>.Success(response);
        }

        public async Task<Result<SubscriptionResponse>> RenewAsync()
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result<SubscriptionResponse>.Failure(ProviderError.NotFound);

            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ProviderId == provider.Id
                                       && s.SubscriptionStatus == SubscriptionStatus.Active
                                       && s.EndAt > DateTime.UtcNow);

            if (subscription is null)
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotFound);

            subscription.EndAt = subscription.EndAt.AddMonths(1);

            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0
                ? Result<SubscriptionResponse>.Success(_mapper.Map<SubscriptionResponse>(subscription))
                : Result<SubscriptionResponse>.Failure(SubscriptionError.NotUpdated);
        }
        public async Task<Result<SubscriptionResponse>> UpgradeAsync(SubscriptionRequest request)
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result<SubscriptionResponse>.Failure(ProviderError.NotFound);

            var newPlan = request.PlanName.Trim().ToLowerInvariant().Replace(" ", "");

            if (!PlanPrices.Prices.TryGetValue(newPlan, out var newPrice))
                return Result<SubscriptionResponse>.Failure(SubscriptionError.InvalidPlan);

            var now = DateTime.UtcNow;

            var current = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ProviderId == provider.Id
                                       && s.SubscriptionStatus == SubscriptionStatus.Active
                                       && s.EndAt > now);

            if (current is null)
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotFound);

            if (current.PlanName == newPlan)
                return Result<SubscriptionResponse>.Failure(SubscriptionError.SamePlan);

            var oldPrice = PlanPrices.Prices[current.PlanName];

            if (newPrice <= oldPrice)
                return Result<SubscriptionResponse>.Failure(SubscriptionError.NotAnUpgrade);


            var remaining = current.EndAt - now;


            current.SubscriptionStatus = SubscriptionStatus.Upgraded;

            var upgraded = new Subscription
            {
                ProviderId = provider.Id,
                PlanName = newPlan,
                StartAt = now,
                EndAt = now.AddMonths(1).Add(remaining),
                SubscriptionStatus = SubscriptionStatus.Upgraded
            };

            await _context.Subscriptions.ExecuteUpdateAsync(
                set => set.SetProperty(s => s.PlanName, newPlan).
                SetProperty(s => s.SubscriptionStatus , SubscriptionStatus.Upgraded));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
            {
                return Result<SubscriptionResponse>.Failure(SubscriptionError.AlreadySubscribed);
            }

            return Result<SubscriptionResponse>.Success(_mapper.Map<SubscriptionResponse>(upgraded));
        }
        public async Task<Result> DowngradeAsync(SubscriptionRequest request)
        {
            var ownerId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(ownerId))
                return Result.Failure(SubscriptionError.NotAuthorized);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OwnerId == ownerId && !p.IsDeleted);

            if (provider is null)
                return Result.Failure(ProviderError.NotFound);

            var newPlan = request.PlanName.Trim().ToLowerInvariant().Replace(" ", "");

            if (!PlanPrices.Prices.TryGetValue(newPlan, out var newPrice))
                return Result.Failure(SubscriptionError.InvalidPlan);

            var current = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ProviderId == provider.Id
                                       && s.SubscriptionStatus == SubscriptionStatus.Upgraded
                                       && s.EndAt > DateTime.UtcNow);

            if (current is null)
                return Result.Failure(SubscriptionError.NotFound);

            if (current.PlanName == newPlan)
                return Result.Failure(SubscriptionError.SamePlan);

            if (newPrice >= PlanPrices.Prices[current.PlanName])
                return Result.Failure(SubscriptionError.NotADowngrade);

            current.PendingPlanName = newPlan;

            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0
                ? Result.Success()
                : Result.Failure(SubscriptionError.NotUpdated);
        }
    }
}
