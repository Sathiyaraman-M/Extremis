using BlazorSlice.Dialog.Extensions;
using BlazorSlice.Toast.Extensions;
using Extremis.Client.Services.HttpClients;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Extremis.Client.Extensions;

public static class HostingExtensions
{
    public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        return builder;
    }

    public static WebAssemblyHostBuilder ConfigureServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.ConfigureHttpClients(builder.HostEnvironment.BaseAddress);
        builder.Services.AddApiAuthorization();
        builder.Services.AddBlazorSliceToast();
        builder.Services.AddBlazorSliceDialog();
        builder.Services.AddClientServices();
        return builder;
    }

    private static void ConfigureHttpClients(this IServiceCollection services, string baseAddress)
    {
        services.AddHttpClient("Extremis.ServerAPI", client => client.BaseAddress = new Uri(baseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Extremis.ServerAPI"));
        services.AddHttpClient<PublicHttpClient>(client => client.BaseAddress = new Uri(baseAddress));
    }

    public static WebAssemblyHost InvokeStartupServices(this WebAssemblyHost host)
    {
        return host;
    }
}