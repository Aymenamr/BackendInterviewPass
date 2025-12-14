using InterviewPass.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace InterviewPass.Infrastructure.Middlewares
{
	//TODO : Move this to instrastrucutre and rename the project according to the convension
	public class ExceptionHandler
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandler> _logger;

		public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
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
		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			if (exception is NotFoundException)
			{
				context.Response.StatusCode = StatusCodes.Status404NotFound;
				return context.Response.WriteAsync(JsonConvert.SerializeObject(new
				{
					msg = exception.Message,
					status = 404
				}));
			}
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
}
