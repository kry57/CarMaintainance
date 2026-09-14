namespace CarMaintenance.Infrastructre.Configuration
{
    public class ProductEntityType : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(125);
            builder.Property(p => p.Price).IsRequired();
            builder.HasOne(p => p.Provider).WithMany(p => p.Products).HasForeignKey(p => p.ProviderId);
        }
    }
}
