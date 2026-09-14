namespace CarMaintenance.Infrastructre.Configuration
{
    public class ReviewEntityType : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Comment).IsRequired().HasMaxLength(125);
            builder.Property(r => r.Rating).IsRequired();
            builder.HasOne(r => r.Provider).WithMany(p => p.Reviews).HasForeignKey(r => r.ProviderId);
            builder.Property(r => r.CustomerId).IsRequired();
        }
    }
}
