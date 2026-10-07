namespace CarMaintenance.Application.DTOs.Response
{
    public class CustomerRegistrationRequestResponse
    {
        public string CustomerId { get; set; } = string.Empty;
        public IEnumerable<RegistrationRequestResponse> RegistrationRequestResponses { get; set; } = [];
    }
}
