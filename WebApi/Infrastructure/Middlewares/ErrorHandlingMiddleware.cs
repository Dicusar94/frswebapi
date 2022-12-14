using Bussines.Abstraction.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has occured.");

                switch (ex)
                {
                    case EntityNullReferenceException _:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidateException _:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await CreateExceptionResponseAsync(context, ex);
            }
        }

        private static Task CreateExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
            }.ToString());
        }
    }
}
