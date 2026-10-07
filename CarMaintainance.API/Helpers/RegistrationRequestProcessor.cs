using CarMaintenance.Application.ErrorProvider.RegistrationsRequestProvider;
using Hangfire;
using Serilog.Core;

namespace CarMaintainance.API.Helpers
{
    public class RegistrationRequestProcessor(ApplicationDbContext context, ILogger<RegistrationRequestProcessor> logger)
    {
        private static readonly TimeSpan Slot = TimeSpan.FromHours(1);

        [AutomaticRetry(Attempts = 3)]
        public async Task ProcessAsync(int requestId)
        {
            var request = await context.RegistrationRequests
                .FirstOrDefaultAsync(r => r.Id == requestId && r.RequestStatus == RequestStatus.Pending);

            
            if (request is null)
                return;

            
            if (request.AppointmentTime <= DateTime.UtcNow)
            {
                request.RequestStatus = RequestStatus.Rejected;
                request.RejectionReason = RejectionReasons.AppointmentPassed;
                logger.LogInformation(
    "Request {RequestId} rejected for provider {ProviderId}: slot booked at {Appointment}",
    request.Id, request.ProviderId, request.AppointmentTime);
                await context.SaveChangesAsync();
                return;
            }

            var start = request.AppointmentTime;
            var end = start + Slot;

            var hasConflict = await context.RegistrationRequests.AnyAsync(r =>
                r.ProviderId == request.ProviderId &&
                r.Id != request.Id &&
                r.RequestStatus == RequestStatus.Accepted &&
                r.AppointmentTime < end &&
                r.AppointmentTime > start - Slot);

            if (hasConflict)
            {
                request.RequestStatus = RequestStatus.Rejected;
                request.RejectionReason = RejectionReasons.TimeSlotBooked;
                logger.LogInformation(
    "Request {RequestId} accepted for provider {ProviderId} at {Appointment}",
    request.Id, request.ProviderId, request.AppointmentTime);
            }
            else
            {
                request.RequestStatus = RequestStatus.Accepted;
            }

            await context.SaveChangesAsync();
        }

        public async Task ProcessAllPendingAsync()
        {
            var ids = await context.RegistrationRequests
                .Where(r => r.RequestStatus == RequestStatus.Pending)
                .OrderBy(r => r.RegisteredAt)
                .Select(r => r.Id)
                .ToListAsync();

            foreach (var id in ids)
                await ProcessAsync(id);
        }
    }
}