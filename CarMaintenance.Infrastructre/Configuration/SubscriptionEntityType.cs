namespace CarMaintenance.Infrastructre.Configuration
{
    public class SubscriptionEntityType : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.PlanName).IsRequired().HasMaxLength(50);
            builder.HasOne(s => s.Provider).WithMany(s => s.Subscriptions).HasForeignKey(s => s.ProviderId);
        }
    }
}
