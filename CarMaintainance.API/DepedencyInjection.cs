using CarMaintainance.API.JWTProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Auth;
using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Application.Services.Products;
using CarMaintenance.Application.Services.ProviderCategoriesTypes;
using CarMaintenance.Application.Services.Providers;
using CarMaintenance.Application.Services.RegistrationsRequests;
using CarMaintenance.Application.Services.Reviews;
using CarMaintenance.Application.Services.Subscriptions;
using CarMaintenance.Infrastructre.Context;
using CarMaintenance.Infrastructre.Identity;
using CarMaintenance.Infrastructre.Implementations;
using CarMaintenance.Infrastructre.JWT;
using CarMaintenance.Infrastructre.Mapping;
using CarMaintenance.Infrastructre.OptionsPattern;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }
        public static IServiceCollection AddHttpAccessorHandMade(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddProblemDetails();
            return services;
        }
        public static IServiceCollection AddResolverForInterfacesHandMade(this IServiceCollection services)
        {
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICarsRepository, CarRepository>();
            services.AddScoped<ISubscriptionsRepository, SubscriptionRepository>();
            services.AddScoped<IRegistrationRequestsRepository, RegistrationsRequestsRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IProviderCategoryTypesRepository, ProviderCategoryRepository>();

            // Services
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IAuthService, AuthService>();

            // HTTP context / current user
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
        public static IServiceCollection AddAutoMapperHandMade(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(ApplicaionUserMapped).Assembly);
            services.AddAutoMapper(cfg => { }, typeof(ProviderMapped).Assembly);
            return services;
        }
        public static IServiceCollection AddJWTHandMade(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(JWTOptions.SectionName).Get<JWTOptions>();
            services.Configure<JWTOptions>(configuration.GetSection(JWTOptions.SectionName));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience
                };
            });
            return services;
        }
      
    }
}
