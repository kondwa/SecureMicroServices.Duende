using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.ComponentModel.Design;

namespace ID.Serve
{
    public static class Configs
    {
        public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "client.console",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("console-secret".Sha256())
                },
                AllowedScopes = { "service.one.read","service.two.write" }
            },
            new Client
            {
                ClientId = "client.mvc",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RedirectUris =
                {
                    "https://localhost:7003/signin-oidc",
                    "http://localhost:5003/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7003/signout-callback-oidc",
                    "http://localhost:5003/signout-callback-oidc"
                },
                ClientSecrets =
                {
                    new Secret("mvc-secret".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "service.one.read",
                    "service.two.write",
                    IdentityServerConstants.StandardScopes.OfflineAccess
                },
                AllowOfflineAccess = true
            }
        ];
        public static IEnumerable<ApiResource> ApiResources =>
        [
            new ApiResource("service.one", "Service One")
            {
                Scopes = { "service.one.read" }
            },
            new ApiResource("service.two", "Service Two")
            {
                Scopes = { "service.two.write" }
            }
        ];
        public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("service.one.read", "Service One Read"),
            new ApiScope("service.two.write", "Service Two Write"),
            new ApiScope(IdentityServerConstants.StandardScopes.OpenId,"Open Id"),
            new ApiScope(IdentityServerConstants.StandardScopes.Profile,"Profile")
        ];

        public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        ];

    }
}
