﻿using System.Net;
using System.Text.Json;

namespace LoofNex.API.Middleware
{
	public class GlobalExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionMiddleware> _logger;

		public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
				_logger.LogError(ex, "Unhandled exception occurred");

				await HandleExceptionAsync(context, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = exception switch
			{
				ArgumentException => (int)HttpStatusCode.BadRequest,
				KeyNotFoundException => (int)HttpStatusCode.NotFound,
				UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
				_ => (int)HttpStatusCode.InternalServerError
			};

			var response = new
			{
				StatusCode = context.Response.StatusCode,
				Message = exception.Message,
				Detail = exception.InnerException?.Message
			};

			var json = JsonSerializer.Serialize(response);
			await context.Response.WriteAsync(json);
		}
	}
}
