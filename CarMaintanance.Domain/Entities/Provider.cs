namespace CarMaintanance.Domain.Entities
{
    public class Provider : BaseEntity
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

    }
}
