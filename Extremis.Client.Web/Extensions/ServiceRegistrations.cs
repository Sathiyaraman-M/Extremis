using Extremis.Client.Services;

namespace Extremis.Client.Extensions;

public static class ServiceRegistrations
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddTransient<UserService>();
        return services;
    }
}