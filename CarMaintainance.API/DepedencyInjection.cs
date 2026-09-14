using Microsoft.AspNetCore.Identity;
using CarMaintenance.Infrastructre.Context;
using CarMaintenance.Infrastructre.Identity;
using Microsoft.EntityFrameworkCore;


namespace CarMaintainance.API
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddDbContextAndResolverHandMade(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            ); 
            return services;
        }
        public static IServiceCollection AddIdentityAndStoresHandMade(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
            return services;
        }
        public static IServiceCollection AddHttpAccessorHandMade(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
