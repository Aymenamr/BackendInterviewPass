using InterviewPass.WebApi.Models;
using Newtonsoft.Json;
using System.Net;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex); 
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new JsonExceptionResponse
        {
            msg = "An unexpected error occurred. Please try again later.",
            close = 0,
            status = (int)HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.status;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
