using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.AddInterceptors(new AuditableEntityInterceptor());
            opts.UseSqlServer(connectionString, b=> b.MigrationsAssembly("Ordering.API"));
        });
        return services;
    }
    
    public static async Task<IApplicationBuilder> MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
        try
        {
            await context.Database.MigrateAsync();
            await DbSeeder.SeedAsync(context);
                
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
            logger.LogInformation("Database migration and seeding completed successfully");
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
            logger.LogError(ex, "An error occurred during database migration or seeding");
            throw;
        }
            
        return app;
    }
    
}
