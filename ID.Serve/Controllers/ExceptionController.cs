using ID.Serve.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ID.Serve.Controllers
{
    public class ExceptionController : Controller
    {
        [Route("/exception")]
        public IActionResult Error()
        {
            var statusFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View("Error", new ExceptionDto
            {
                StatusCode = 500,
                Message = "Server Error",
                StackTrace = exceptionFeature?.Error.StackTrace??string.Empty,
                Source = statusFeature?.OriginalPath ?? string.Empty,
            });
        }
        [Route("/exception/{code:int}")]
        public IActionResult Error(int code)
        {
            var statusFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            string message = code switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                405 => "Method Not Allowed",
                408 => "Request Timeout",
                409 => "Conflict",
                410 => "Gone",
                415 => "Unsupported Media Type",
                429 => "Too Many Requests",
                500 => "Server Error",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                _ => "Error"
            };
            return View("Error", new ExceptionDto
            {
                StatusCode = code,
                Message = exceptionFeature?.Error.Message?? message,
                StackTrace = exceptionFeature?.Error.StackTrace ?? string.Empty,
                Source = statusFeature?.OriginalPath ?? string.Empty,
            });
        }
    }
}
