using Client.MVC.Apis;
using Client.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.MVC.Controllers
{
    public class AccountController(IIdServiceAPI idService) : Controller
    {


        
        public async Task<IActionResult> LogoutAsync()
        {
            await idService.Logout();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Login(string returnUrl = "/")
        {
            return View(new UserCredentialsDto { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserCredentialsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            
            dto.GrantType = OpenIdConnectGrantTypes.Password;
            dto.Scope = "openid profile service.one.read service.two.write offline_access";
            dto.ClientId = "client.mvc";
            dto.ClientSecret = "mvc-secret";
            
            try
            {
                var response =await idService.GetToken(dto.FormData);
                if (!response.IsSuccessful)
                {
                    ModelState.AddModelError("", response.Error.Message);
                    return View(dto);
                }
                var token = response.Content;
                if (token.IsError)
                {
                    ModelState.AddModelError("", token.ErrorDescription ?? string.Empty);
                    return View(dto);
                }
                if (token.AccessToken == null)
                {
                    ModelState.AddModelError("", "Invalid credentials");
                    return View(dto);
                }
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token.AccessToken);
                
                var claims = jwt.Claims.ToList();
                
                claims.Add(new Claim("access_token", token.AccessToken));
                claims.Add(new Claim("refresh_token", token.RefreshToken));
                claims.Add(new Claim("expires_at", DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString()));
                claims.Add(new Claim("scope", token.Scope));
                claims.Add(new Claim("id_token", token.IdToken));
                
                var cookieClaimsId = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe,
                    RedirectUri = dto.ReturnUrl
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(cookieClaimsId), authProperties);
                
                return LocalRedirect(dto.ReturnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegisterDto { });
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await idService.Register(dto);
                if (!response.IsSuccessful)
                {
                    ModelState.AddModelError("", response.Error.Message);
                    return View(dto);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View(dto);
        }

        [Route("/signin-oidc")]
        public IActionResult SignInOidc()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }
        [Route("/signout-callback-oidc")]
        public IActionResult SignOutOidc()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }
    }
}
