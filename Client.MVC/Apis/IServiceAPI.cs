using Client.MVC.Models;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics.Contracts;

namespace Client.MVC.Apis
{
    public interface IServiceOneAPI
    {
        [Get("/api/one/read")]
        Task<string> ReadAsync();
    }
    public interface IServiceTwoAPI
    {
        [Post("/api/two/write")]
        Task<string> WriteAsync([Body] WriteRequestDto dto);
    }
    
    public interface IIdServiceAPI
    {
        [Post("/connect/token")]
        Task<ApiResponse<TokenResponseDto>> GetToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string,string> formData);

        [Post("/api/account/register")]
        Task<ApiResponse<string>> Register([Body] UserRegisterDto dto);
        [Get("/api/account/change-password")]
        Task<ApiResponse<string>> ChangePassword();
        [Put("/api/account/update")]
        Task<ApiResponse<string>> Update([Body] UserProfileDto dto);

        [Post("/api/account/logout")]
        Task<ApiResponse<string>> Logout();
    }
}
