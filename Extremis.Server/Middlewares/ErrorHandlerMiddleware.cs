using System.Net;
using System.Text.Json;
using Extremis.Wrapper;

namespace Extremis.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = await Result<string>.FailAsync(e.Message);
            response.StatusCode = e switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,//Not Found Error
                _ => (int)HttpStatusCode.InternalServerError,//Unhandled Error
            };
            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}