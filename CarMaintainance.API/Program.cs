
using Scalar.AspNetCore;

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

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerDocumentation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();
                app.MapSwagger();
                app.MapSwaggerUI();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
