namespace CarMaintenance.Infrastructre.Configuration
{
    public class RegistrationRequestEntityType : IEntityTypeConfiguration<RegistrationRequest>
    {
        public void Configure(EntityTypeBuilder<RegistrationRequest> builder)
        {
            builder.HasKey(rq => rq.Id);
            builder.Property(re => re.CustomerId).IsRequired();
            builder.HasOne(re => re.Provider).WithMany(p => p.RegistrationRequests).HasForeignKey(re => re.ProviderId);
        }
    }
}
