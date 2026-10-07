
using CarMaintainance.API.Helpers;
using Hangfire;
using Scalar.AspNetCore;
using Serilog;
using System.Text.Json.Serialization;


namespace CarMaintainance.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContextAndResolverHandMade(builder.Configuration);
            builder.Services.AddIdentityAndStoresHandMade();
            builder.Services.AddHttpAccessorHandMade();
            builder.Services.AddResolverForInterfacesHandMade();
            builder.Services.AddAutoMapperHandMade();
            builder.Services.AddJWTHandMade(builder.Configuration);
            builder.Services.AddHangFireHandMade(builder.Configuration);
            builder.Services.AddScalarHandMade();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerDocumentation();
            

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                });
     //Serilog 
     builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

            var app = builder.Build();
            app.Services.GetRequiredService<IRecurringJobManager>().AddOrUpdate<ISubscriptionExpiryJob>( "expire-subscriptions", job => job.ExpireSubscriptionsAsync(), Cron.Minutely);
           app.Services.GetRequiredService<IRecurringJobManager>().AddOrUpdate<RegistrationRequestProcessor>( "process-pending-requests",x => x.ProcessAllPendingAsync(),Cron.Minutely);
           app.UseHangfireDashboard("/hangfire");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                
                app.MapSwagger();
                app.MapSwaggerUI();
                app.MapOpenApi();

                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("Car Maintenance API")
                        .WithHttpBearerAuthentication(
                            bearer =>
                            {
                                bearer.Token = "PUT_YOUR_TOKEN_HERE";
                            });
                });
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    DashboardTitle = "Car Maintenance Jobs"
                });
            }
            app.UseSerilogRequestLogging();

            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
