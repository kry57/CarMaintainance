using CarMaintenance.Infrastructre.Identity;
using CarMaintenance.Infrastructre.Seeding;

namespace CarMaintenance.Infrastructre.Configuration
{
    public class ApplicationRoleEntityType : IEntityTypeConfiguration<ApplicationRole>
    {
     
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            //Seeding
            builder.HasData([
                new ApplicationRole{
                    Id = DefaultRole.AdminRoleId ,
                    Name = DefaultRole.Admin,
                    ConcurrencyStamp = DefaultRole.AdminRoleConcurrencyStamp,
                    IsDefault = false ,
                    IsDeleted = false ,
                    NormalizedName = DefaultRole.Admin.ToUpper()
                },
                new ApplicationRole{
                    Id = DefaultRole.ProviderRoleId ,
                    Name = DefaultRole.Provider,
                    ConcurrencyStamp = DefaultRole.ProviderRoleConcurrencyStamp,
                    IsDefault = false ,
                    IsDeleted = false ,
                    NormalizedName = DefaultRole.Provider.ToUpper()
                },
                     new ApplicationRole{
                    Id = DefaultRole.MemberRoleId ,
                    Name = DefaultRole.Member,
                    ConcurrencyStamp = DefaultRole.MemberRoleConcurrencyStamp,
                    IsDefault = true ,
                    IsDeleted = false ,
                    NormalizedName = DefaultRole.Member.ToUpper()
                }

                ]);

        }
    }
}
