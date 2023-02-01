using Extremis.Proposals;
using Extremis.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Extremis.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProposalService, ProposalService>();
        return services;
    }
}