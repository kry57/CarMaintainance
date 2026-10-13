using CarMaintenance.Infrastructre.Seeding;
using Microsoft.AspNetCore.Identity;

public class RoleClaimEntityType : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var permissions = Permissions.GetAllPermissions();
        var claims = new List<IdentityRoleClaim<string>>();

        var id = 1;

        
        foreach (var permission in permissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                ClaimType = Permissions.Type,
                ClaimValue = permission,
                RoleId = DefaultRole.AdminRoleId
            });
        }

        
        var providerPermissions = new[]
        {
        
        Permissions.ViewProducts,
        Permissions.AddProducts,
        Permissions.UpdateProducts,
        Permissions.DeleteProducts,
        Permissions.ToggleProductsStatus,
        Permissions.ViewProductImages,
        Permissions.AddProductImages,
        Permissions.DeleteProductImages,
        
        Permissions.ViewProviders,
        Permissions.UpdateProviders,

        Permissions.ViewProviderRequests,
        Permissions.RespondToRequests,

        Permissions.ViewSubscriptions,
        Permissions.AddSubscriptions,
        Permissions.CancelSubscriptions,
        Permissions.RenewSubscriptions,
        Permissions.UpgradeSubscriptions,
        Permissions.DowngradeSubscriptions,
        
        Permissions.ViewReviews

        };

        foreach (var permission in providerPermissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                ClaimType = Permissions.Type,
                ClaimValue = permission,
                RoleId = DefaultRole.ProviderRoleId
            });
        }
        
        var customerPermissions = new[]
        {
        
        
        Permissions.ViewCars,
        Permissions.AddCars,
        Permissions.UpdateCars,
        Permissions.DeleteCars,
        Permissions.ToggleCarsStatus,

        Permissions.UseDiagnosis,
        
        Permissions.ViewReviews,
        Permissions.AddReviews,
        Permissions.UpdateReviews,
        Permissions.DeleteReviews,
        
        Permissions.ViewProducts,
        Permissions.ViewProductImages,
        Permissions.ViewProviders


        };

        foreach (var permission in customerPermissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                ClaimType = Permissions.Type,
                ClaimValue = permission,
                RoleId = DefaultRole.MemberRoleId
            });
        }

        builder.HasData(claims);
    }
}