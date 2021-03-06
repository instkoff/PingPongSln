using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PingPong.Shared.Models.Responses;
using PongApp.Domain.Models.Exceptions;

namespace PongApp.Utils
{
    /// <summary>
    /// Промежуточное ПО для работы с исключениями.
    /// Глобально перехватывает и возвращает ответ.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An unexpected error has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                AddMessageException _ => HttpStatusCode.InternalServerError,
                MessageNotFoundException _ => HttpStatusCode.BadRequest,
                UserNotFoundException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new ErrorResponse
            {
                ErrorMessage = ex.Message,
                Status = 0
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}