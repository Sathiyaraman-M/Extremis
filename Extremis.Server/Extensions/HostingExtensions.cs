using Extremis.DbContexts;
using Extremis.Middlewares;
using Extremis.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

namespace Extremis.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddIdentityServer()
            .AddApiAuthorization<AppUser, AppDbContext>();
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            });
        builder.Services.AddAuthorization();
        builder.Services.AddApplicationServices();
        builder.Services.AddInternalServices();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder.Build();
    }

    public static WebApplication ConfigureMiddlewares(this WebApplication app)
    {
        app.UseCors();

        app.UseMiddleware<ErrorHandlerMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseBlazorFrameworkFiles();
        app.UseHttpsRedirection();
        
        app.UseStaticFiles(new StaticFileOptions()
        {
            RequestPath = new PathString("/Files"),
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files"))
        });

        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Extremis");
            options.DisplayRequestDuration();
            options.RoutePrefix = "swagger";
        });
        return app;
    }
}