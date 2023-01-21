global using CrudApp.Extensions;
using Crud.Shared;
using CrudApp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
JwtSecurityTokenHandler.DefaultMapInboundClaims = true;
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", options =>
	{
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
	})
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "mvc";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.Scope.Add("api1");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.ClaimActions.MapUniqueJsonKey("Id", "Id");
        options.ClaimActions.MapUniqueJsonKey("Email", "Email");
        options.ClaimActions.MapUniqueJsonKey("Name", "Name");
        options.ClaimActions.MapUniqueJsonKey("Username", "Username");
    });
builder.Services.AddHttpClient("CrudClient", options =>
{
	options.BaseAddress = new Uri("https://localhost:7137");
	options.Timeout = new TimeSpan(0, 0, 10000);
	options.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
}).ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler());
builder.Services.AddHttpClient("IS4Client", options =>
{
    options.BaseAddress = new Uri("https://localhost:5001");
    options.Timeout = new TimeSpan(0, 0, 10000);
    options.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
}).ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler());

builder.Services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
builder.Services.AddScoped(typeof(ICrudClient), typeof(CrudClient));

builder.Services.AddShared();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.Run();
