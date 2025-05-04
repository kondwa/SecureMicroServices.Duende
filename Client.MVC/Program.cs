using Client.MVC.Apis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Refit;
using NuGet.Protocol;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
   // options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Account/Login";  // Customize your login path here
    options.LogoutPath = "/Account/Logout";
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://localhost:7000";
    options.ClientId = "client.mvc";
    options.ClientSecret = "mvc-secret";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.Scope.Add("service.one.read");
    options.Scope.Add("service.two.write");
    options.Scope.Add("offline_access");
    options.CallbackPath = new PathString("/signin-oidc");
    options.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
    options.GetClaimsFromUserInfoEndpoint = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AccessTokenHandler>();
builder.Services.AddTransient<LoggingHandler>();

var refitSettings = new RefitSettings(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
}));

builder.Services.AddRefitClient<IServiceOneAPI>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7001"))
    .AddHttpMessageHandler<AccessTokenHandler>()
    .AddHttpMessageHandler<LoggingHandler>();

builder.Services.AddRefitClient<IServiceTwoAPI>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7002"))
    .AddHttpMessageHandler<AccessTokenHandler>()
    .AddHttpMessageHandler<LoggingHandler>();

builder.Services.AddRefitClient<IIdServiceAPI>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7000"))
    .AddHttpMessageHandler<AccessTokenHandler>()
    .AddHttpMessageHandler<LoggingHandler>();

builder.WebHost.UseUrls("https://localhost:7003;http://localhost:5003");

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
