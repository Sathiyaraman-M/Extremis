global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using Extremis.Wrapper;
global using BlazorSlice.Toast;

using Extremis.Client.Extensions;

WebAssemblyHostBuilder.CreateDefault(args)
    .AddRootComponents()
    .ConfigureServices()
    .Build()
    .InvokeStartupServices()
    .RunAsync();