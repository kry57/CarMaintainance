namespace CarMaintenance.Infrastructre.Configuration
{
    public class ProviderCategoryTypeEntityType : IEntityTypeConfiguration<ProviderCategoryType>
    {
        public void Configure(EntityTypeBuilder<ProviderCategoryType> builder)
        {
            builder.HasKey(pc => new { pc.ProviderId, pc.ProviderCategory });
            builder.Ignore(pc => pc.Id);
            builder.HasOne(pc => pc.Provider).WithMany(pc => pc.ProviderCategoryTypes).HasForeignKey(pc => pc.ProviderId);
        }
    }
}
