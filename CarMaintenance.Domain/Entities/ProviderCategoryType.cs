namespace CarMaintenance.Domain.Entities
{
    public class ProviderCategoryType : AuditableEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!; 
        public ProviderCategory ProviderCategory { get; set; }
    }
}
