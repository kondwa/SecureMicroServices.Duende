
namespace Client.MVC.Apis
{
    public class LoggingHandler:DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"-> Request to: {request.RequestUri}");
            Console.WriteLine($"-> Authorization: {request.Headers.Authorization}");

            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine($"<- Response content: {await response.Content.ReadAsStringAsync()}");
            Console.WriteLine($"<- Response status: {response.StatusCode}");
            return response;
        }
    }
}
