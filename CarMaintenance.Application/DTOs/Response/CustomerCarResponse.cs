namespace CarMaintenance.Application.DTOs.Response
{
    public class CustomerCarResponse
    {
        public string CustomerId { get; set; }
        public IEnumerable<CarResponse> CarResponses { get; set; }
    }
}
