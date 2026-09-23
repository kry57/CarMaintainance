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
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Car Maintenance Platform API",
                    Description = "A multi-tenant SaaS API for a provider-based car maintenance marketplace, enabling customers to search for garages and spare-part sellers, manage their vehicles, and handle service requests",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Khaled Yasser",
                        Url = new Uri("https://github.com/kry57/CarMaintainance")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                options.IncludeXmlComments(
                    Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
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
        public static IServiceCollection AddAutoMapperHandMade(
      this IServiceCollection services)
        {
            services.AddAutoMapper(
                cfg => { },
                typeof(ApplicaionUserMapped).Assembly,
                typeof(ProviderMapped).Assembly
            );

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
