using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using FinanceKeeper.Data.Entities;

namespace FinanceKeeper.Middleware
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private const string _defaultErrorMessage = "An error occurred on the server side, please contact support";
        private const string _requestCancelledMessage = "Request was cancelled";
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = new ErrorDetails { ErrorMessage = exception.Message };
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            switch (exception)
            {
                case HttpRequestException request:
                    code = HttpStatusCode.InternalServerError;
                    result.ErrorMessage = request.Message;
                    _logger.LogInformation($"{DateTime.Now} invoke exception with code {code} - {result.ErrorMessage}");
                    break;

                default:
                    _logger.LogError(exception, "Unexpected error occurred");
                    result.ErrorMessage = _defaultErrorMessage;
                    break;
            }

            result.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode;

            var response = JsonSerializer.Serialize(result, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            await context.Response.WriteAsync(response);
        }
    }
}
