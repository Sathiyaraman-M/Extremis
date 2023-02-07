using Extremis.Client.Services;

namespace Extremis.Client.Extensions;

public static class ServiceRegistrations
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddTransient<ChatService>();
        services.AddTransient<ProjectService>();
        services.AddTransient<ProposalService>();
        services.AddTransient<UserService>();
        return services;
    }
}