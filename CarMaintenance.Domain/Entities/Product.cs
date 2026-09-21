namespace CarMaintenance.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PartType PartType { get; set; }
    }
}
