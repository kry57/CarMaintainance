namespace CarMaintenance.Domain.Entities
{
    public class Provider : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } =  string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public string OwnerId { get; set; }= string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<ProviderCategoryType> ProviderCategoryTypes { get; set; } = new List<ProviderCategoryType>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public ICollection<RegistrationRequest> RegistrationRequests { get; set; } = new List<RegistrationRequest>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
