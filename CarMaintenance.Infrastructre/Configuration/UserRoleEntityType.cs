using CarMaintenance.Infrastructre.Seeding;
using Microsoft.AspNetCore.Identity;

namespace CarMaintenance.Infrastructre.Configuration
{
    public class UserRoleEntityType : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //Seeding

            builder.HasData(new IdentityUserRole<string> { 

            UserId = DefaultUser.AdminId ,
            RoleId = DefaultRole.AdminRoleId ,
            
            
            });
            builder.HasData(new IdentityUserRole<string> { 

            UserId = DefaultUser.ProviderId ,
            RoleId = DefaultRole.ProviderRoleId ,
            
            
            });

        }
    }
}
