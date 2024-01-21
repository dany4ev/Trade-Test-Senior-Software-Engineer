using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trade_Test.Models.Constants;
using Trade_Test.Models;

namespace Trade_Test.Utilities.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private const long FILEUPLOADLIMIT = 209715200; // 1677721600 bytes = 1600 MB, 209715200 bytes = 200 MB

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            if (request.ContentLength > 0)
            {
                if (request.ContentLength > FILEUPLOADLIMIT)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new BadRequestObjectResult(new ErrorMessage() { FriendlyMessage = Constants.FilesSizeLimitMessage,
                        StatusCode = (int)HttpStatusCode.InternalServerError });
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
