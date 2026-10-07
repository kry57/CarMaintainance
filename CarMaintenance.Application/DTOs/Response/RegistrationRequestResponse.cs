using CarMaintenance.Domain.Enums;


namespace CarMaintenance.Application.DTOs.Response
{
    public class RegistrationRequestResponse
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int ProviderId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string? RejectionReason { get; set; }

    }
}
