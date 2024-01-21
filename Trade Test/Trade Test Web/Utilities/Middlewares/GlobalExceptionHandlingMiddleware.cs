
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Text.Json;

using Trade_Test.Models.Constants;
using Trade_Test.Models.Enums;
using Trade_Test.Utilities.Extensions;

namespace Trade_Test_Web.Utilities.Middlewares {
    public class GlobalExceptionHandlingMiddleware : IMiddleware {

        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(
            ILogger<GlobalExceptionHandlingMiddleware> logger
            ) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            try {

                await next(context);
            }
            catch (Exception ex) {

                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new() {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = TradeTestConstants.ServerErrorMessage,
                    Title = TradeTestConstants.ServerErrorTitle,
                    Detail = TradeTestConstants.ServerErrorMessageDetail
                };

                var json = JsonSerializer.Serialize(problem);

                await context.Response.WriteAsJsonAsync(json);

                context.Response.ContentType = ContentTypeExtensions.ToValue(ContentTypes.JSON);
            }
        }
    }
}
