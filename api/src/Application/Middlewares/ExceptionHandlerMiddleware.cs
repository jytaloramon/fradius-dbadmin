using System.Net;
using System.Text.Json;
using FradminDomain.Exceptions;

namespace Application.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException e)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = e switch
            {
                EntityConflictException => (int)HttpStatusCode.Conflict,
                _ => response.StatusCode
            };

            await response.WriteAsync(JsonSerializer.Serialize(e.Errors));
        }
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            await response.WriteAsync(JsonSerializer.Serialize(new { message = e.Message }));
        }
    }
}