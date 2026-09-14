namespace CarMaintenance.Infrastructre.Configuration
{
    public class CarEntityType : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.PlateNumber).IsRequired();
            builder.Property(c => c.Year).IsRequired();
            builder.Property(c => c.Model).IsRequired();
            builder.Property(c => c.Make).IsRequired().HasMaxLength(100);
            builder.Property(c => c.CustomerId).IsRequired();
        }
    }
}
