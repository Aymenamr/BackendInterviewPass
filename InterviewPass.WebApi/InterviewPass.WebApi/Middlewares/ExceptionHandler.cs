using InterviewPass.WebApi.Models;
using Newtonsoft.Json;
using System.Net;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandler(RequestDelegate next,ILogger logger)
    {
        _next = next;
        _logger = logger;
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
    private  Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unexpected Exception occured");
        var response = new JsonExceptionResponse
        {
            msg = "An unexpected error occurred. Please contact an administrator.",
            status = (int)HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.status;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
