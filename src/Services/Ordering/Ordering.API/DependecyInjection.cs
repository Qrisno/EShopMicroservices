namespace Ordering.API;

public static class DependencyInjection
{
    // Before Building App
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services;
    }
    
    
    // After Building App
    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}