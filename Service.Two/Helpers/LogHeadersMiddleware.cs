namespace Service.Two.Helpers
{
    public class LogHeadersMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"-> Request to: {context.Request.Path}");
            Console.WriteLine($"-> Authorization: {context.Request.Headers["Authorization"]}");

            await next(context);

            Console.WriteLine($"<- Response status: {context.Response.StatusCode}");
        }
    }
}
