using CarMaintenance.Infrastructre.Identity;
using CarMaintenance.Infrastructre.Seeding;
using Microsoft.AspNetCore.Identity;

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
            //
            builder.OwnsMany(u => u.RefreshTokens).ToTable("RefreshTokens").WithOwner().HasForeignKey("UserId");
            //Seeding
            //var hasher = new PasswordHasher<ApplicationUser>();  => 3mlet 3ndy moshkela -_- 
            builder.HasData(new ApplicationUser
            {

                Id = DefaultUser.AdminId,
                ConcurrencyStamp = DefaultUser.AdminConcurrencyStamp,
                SecurityStamp = DefaultUser.AdminSecurityStamp,
                Email = DefaultUser.AdminEmail,
                NormalizedEmail = DefaultUser.AdminEmail.ToUpper(),
                FullName = "Admin-Carmaintenance",
                UserName = DefaultUser.AdminEmail,
                NormalizedUserName = DefaultUser.AdminEmail.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = DefaultUser.PasswordHasher
            });
            builder.HasData(new ApplicationUser { 
            
                Id = DefaultUser.ProviderId ,
                ConcurrencyStamp = DefaultUser.ProviderConcurrencyStamp,
                SecurityStamp = DefaultUser.ProviderSecurityStamp,
                Email = DefaultUser.ProviderEmail,
                NormalizedEmail = DefaultUser.ProviderEmail.ToUpper(),
                FullName = "Provider-Carmaintenance",
                UserName = DefaultUser.ProviderEmail,
                NormalizedUserName = DefaultUser.ProviderEmail.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = DefaultUser.PasswordHasher
            });
            //builder.HasData(new ApplicationUser
            //{

            //    Id = DefaultUser.MemberId,
            //    ConcurrencyStamp = DefaultUser.MemberConcurrencyStamp,
            //    SecurityStamp = DefaultUser.MemberSecurityStamp,
            //    Email = DefaultUser.MemberEmail,
            //    NormalizedEmail = DefaultUser.MemberEmail.ToUpper(),
            //    FullName = "Member-Carmaintenance",
            //    UserName = DefaultUser.MemberEmail,
            //    NormalizedUserName = DefaultUser.MemberEmail.ToUpper(),
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null!, DefaultUser.Password)
            //});

        }
    }
}
