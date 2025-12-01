using System.Text.Json;
using Delivery.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using FluentValidation;
namespace Delivery.Api.Middleware
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
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            switch (exception)
            {

                case ValidationException ve:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Validation Error";
                    problemDetails.Extensions["errors"] =
                        ve.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;




                case DomainException dex:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Domain Validation Error";
                    problemDetails.Detail = dex.Message;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;


                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }
    }
}
