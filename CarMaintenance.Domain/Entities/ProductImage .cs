namespace CarMaintenance.Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public string ImageUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;


        public bool IsPrimary { get; set; } = false;
    }
}