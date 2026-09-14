namespace CarMaintenance.Infrastructre.Configuration
{
    public class ProviderEntityType : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(250);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.City).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(250);
            builder.Property(p => p.OwnerId).IsRequired();
        }
    }
}
