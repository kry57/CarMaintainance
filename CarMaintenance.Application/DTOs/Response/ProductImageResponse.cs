namespace CarMaintenance.Application.DTOs.Response
{
    public class ProductImageResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string PublicId { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }
}