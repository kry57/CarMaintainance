using AutoMapper;
using Azure.Core;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.ErrorProvider.RegistrationsRequestProvider;
using CarMaintenance.Application.ErrorProvider.UserErrorPrvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.RegistrationsRequests;
using CarMaintenance.Domain.Enums;
using CarMaintenance.Infrastructre.Context;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class RegistrationsRequestsRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : IRegistrationRequestsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<RegistrationRequestResponse>> AddAsync(string customerId,int providerId, RegistrationRequestDto request)
        {

            if (string.IsNullOrEmpty(customerId))
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.NotAuthorized);


            var appointmentTime = request.AppointmentTime.ToUniversalTime();
            if (appointmentTime <= DateTime.UtcNow)
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.InvalidAppointmentTime);

            var hasCar = await _context.Cars.AnyAsync(c => c.CustomerId == customerId);
            if (!hasCar)
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.NoHasCar);

            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == providerId && !p.IsDeleted);
            if (provider is null)
                return Result<RegistrationRequestResponse>.Failure(ProviderError.NotFound);

            var duplicated = await _context.RegistrationRequests
                .AnyAsync(r =>
                    r.CustomerId == customerId &&
                    r.ProviderId == providerId &&
                    r.RequestStatus == RequestStatus.Pending);
            if (duplicated)
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.Duplicated);

            var registrationRequest = new RegistrationRequest
            {
                CustomerId = customerId,
                ProviderId = providerId,
                RequestStatus = RequestStatus.Pending,
                RegisteredAt = DateTime.UtcNow,
                AppointmentTime = appointmentTime
            };

            await _context.RegistrationRequests.AddAsync(registrationRequest);

            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.NotAdded);

            var mapped = _mapper.Map<RegistrationRequestResponse>(registrationRequest);

            return Result<RegistrationRequestResponse>.Success(mapped);
        }

        public async Task<Result<RegistrationRequestResponse>> GetStatusAsync(string callerId, int requestId)
        {
            var request = await _context.RegistrationRequests
                .AsNoTracking()
                .Include(r => r.Provider)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request is null)
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.NotFound);

            
            if (request.CustomerId != callerId && request.Provider.OwnerId != callerId)
                return Result<RegistrationRequestResponse>.Failure(RegistrationsRequestError.NotAllowed);

            return Result<RegistrationRequestResponse>.Success(new RegistrationRequestResponse
            {
                Id = request.Id,
                ProviderId = request.ProviderId,
                CustomerId = request.CustomerId,
                RequestStatus = request.RequestStatus,
                RegisteredAt = request.RegisteredAt,
                AppointmentTime = request.AppointmentTime

            });
        }
        public async Task<Result> CancelAsync(string callerId, int requestId)
        {
            var request = await _context.RegistrationRequests
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request is null)
                return Result.Failure(RegistrationsRequestError.NotFound);

            
            if (request.CustomerId != callerId)
                return Result.Failure(RegistrationsRequestError.NotAllowed);

            
            if (request.RequestStatus is RequestStatus.Rejected or RequestStatus.Cancelled)
                return Result.Failure(RegistrationsRequestError.CannotCancel);


            
            if (request.AppointmentTime <= DateTime.UtcNow || request.AppointmentTime <= DateTime.UtcNow.AddMinutes(45))
                return Result.Failure(RegistrationsRequestError.CannotCancel);

            request.RequestStatus = RequestStatus.Cancelled;
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        public async Task<Result<CustomerRegistrationRequestResponse>> GetMyRequestsAsync(string callerId)
        {
            var requests = await _context.RegistrationRequests
                .AsNoTracking()
                .Where(r => r.CustomerId == callerId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new RegistrationRequestResponse
                {
                    Id = r.Id,
                    ProviderId = r.ProviderId,
                    CustomerId = r.CustomerId,
                    RequestStatus = r.RequestStatus,
                    RegisteredAt = r.RegisteredAt,
                    AppointmentTime = r.AppointmentTime
                })
                .ToListAsync();

            return Result<CustomerRegistrationRequestResponse>.Success(new CustomerRegistrationRequestResponse
            {
                CustomerId = callerId,
                RegistrationRequestResponses = requests
            });
        }

        public async Task<Result<IEnumerable<CustomerRegistrationRequestResponse>>> GetProviderRequestsAsync(string callerId, int providerId, RequestStatus? status)
        {
            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == providerId && !p.IsDeleted);

            if (provider is null)
                return Result<IEnumerable<CustomerRegistrationRequestResponse>>.Failure(ProviderError.NotFound);

            
            if (provider.OwnerId != callerId)
                return Result<IEnumerable<CustomerRegistrationRequestResponse>>.Failure(RegistrationsRequestError.NotAllowed);

            var query = _context.RegistrationRequests
                .AsNoTracking()
                .Where(r => r.ProviderId == providerId);

            if (status.HasValue)
                query = query.Where(r => r.RequestStatus == status.Value);

            var requests = await query
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new RegistrationRequestResponse
                {
                    Id = r.Id,
                    ProviderId = r.ProviderId,
                    CustomerId = r.CustomerId,
                    RequestStatus = r.RequestStatus,
                    RegisteredAt = r.RegisteredAt,
                    AppointmentTime = r.AppointmentTime
                })
                .ToListAsync();

            var grouped = requests
                .GroupBy(r => r.CustomerId)
                .Select(g => new CustomerRegistrationRequestResponse
                {
                    CustomerId = g.Key,
                    RegistrationRequestResponses = g.ToList()
                })
                .ToList();

            return Result<IEnumerable<CustomerRegistrationRequestResponse>>.Success(grouped);
        }
        public async Task<Result<IEnumerable<BookedSlotResponse>>> GetBookedSlotsAsync(int providerId, DateOnly date)
        {
            var providerExists = await _context.Providers
                .AnyAsync(p => p.Id == providerId && !p.IsDeleted);
            if (!providerExists)
                return Result<IEnumerable<BookedSlotResponse>>.Failure(ProviderError.NotFound);

            var dayStart = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            var dayEnd = dayStart.AddDays(1);

            var starts = await _context.RegistrationRequests
                .AsNoTracking()
                .Where(r => r.ProviderId == providerId
                         && r.RequestStatus == RequestStatus.Accepted
                         && r.AppointmentTime >= dayStart
                         && r.AppointmentTime < dayEnd)
                .OrderBy(r => r.AppointmentTime)
                .Select(r => r.AppointmentTime)
                .ToListAsync();

            var slots = starts.Select(s => new BookedSlotResponse
            {
                Start = s,
                End = s + RegistrationRules.SlotDuration
            });

            return Result<IEnumerable<BookedSlotResponse>>.Success(slots);
        }
    }
}
