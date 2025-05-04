
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Client.MVC.Apis
{
    public class AccessTokenHandler:DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public AccessTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = httpContextAccessor.HttpContext;
            if (context != null)
            {
                var accessToken = context.User?.FindFirstValue("access_token");
                
                Console.WriteLine($"Access Token: {accessToken}");
                
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
