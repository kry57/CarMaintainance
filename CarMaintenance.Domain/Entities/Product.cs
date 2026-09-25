namespace CarMaintenance.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PartType PartType { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public int Quantity { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public ICollection<ProductImage> Images { get; set; } = [];

    }
}
