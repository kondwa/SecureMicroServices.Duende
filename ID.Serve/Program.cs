using ID.Serve;
using ID.Serve.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
    options.KeyManagement.Enabled = false;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryClients(Configs.Clients)
    .AddInMemoryApiScopes(Configs.ApiScopes)
    .AddInMemoryApiResources(Configs.ApiResources)
    .AddDeveloperSigningCredential();

builder.WebHost.UseUrls("https://localhost:7000;http://localhost:5000");

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler("/exception"); 
app.UseStatusCodePagesWithReExecute("/exception/{0}"); 

app.UseHttpsRedirection();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStatusCodePages();

app.Run();
