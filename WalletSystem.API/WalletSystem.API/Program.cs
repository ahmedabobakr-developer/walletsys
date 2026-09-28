
using Microsoft.EntityFrameworkCore;
using WalletSystem.API.Data;

namespace WalletSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<WalletDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("WalletDatabase")
                    ?? throw new InvalidOperationException(
                        "The 'WalletDatabase' connection string is not configured.")));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<WalletDbContext>();
                await DevelopmentDataSeeder.SeedAsync(dbContext);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
