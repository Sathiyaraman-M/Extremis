using Extremis.DbContexts;
using Extremis.Users;

namespace Extremis.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer()
            .AddApiAuthorization<AppUser, AppDbContext>();
        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
            });
        services.AddAuthorization();
    }
    
    public static void AddInternalServices(this IServiceCollection services)
    {
        services.AddRepositoryServices();
    }
}