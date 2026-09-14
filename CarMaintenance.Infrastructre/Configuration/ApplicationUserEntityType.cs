using CarMaintenance.Infrastructre.Identity;

namespace CarMaintenance.Infrastructre.Configuration
{
    public class ApplicationUserEntityType : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(128);
            builder.HasMany(u => u.Providers).WithOne().HasForeignKey(p => p.OwnerId);
            builder.HasMany(u => u.Cars).WithOne().HasForeignKey(p => p.CustomerId);
            builder.HasMany(u => u.Reviews).WithOne().HasForeignKey(p => p.CustomerId);
            builder.HasMany(u => u.Registrations).WithOne().HasForeignKey(p => p.CustomerId);
        }
    }
}
