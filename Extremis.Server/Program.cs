global using System.Security.Claims;
global using IdentityModel;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
using Extremis.Extensions;

WebApplication
    .CreateBuilder(args)
    .ConfigureServices()
    .ConfigureMiddlewares()
    .Run();