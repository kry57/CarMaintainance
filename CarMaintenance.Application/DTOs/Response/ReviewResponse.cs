using CarMaintenance.Domain.Entities;

namespace CarMaintenance.Application.DTOs.Response
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;

        public int ProviderId { get; set; }
        

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
