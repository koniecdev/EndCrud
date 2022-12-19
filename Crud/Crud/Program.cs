using Crud;
using Crud.Application.Common.Interfaces;
using Crud.Infrastructure;
using Crud.Persistance;
using Crud.Service;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

WebApplicationBuilder? builder = null;
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
try
{
	Log.Information("Application is starting up");
	builder = WebApplication.CreateBuilder(args);
}
catch (Exception ex)
{
	Log.Fatal(ex, "Could not start up application");
}
finally
{
	Log.CloseAndFlush();
}
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
	options.AddPolicy("MyOrigins", policy =>
	{
		policy.WithOrigins("https://localhost:5001").AllowAnyMethod().AllowAnyHeader();
		policy.WithOrigins("https://localhost:7137").AllowAnyMethod().AllowAnyHeader();
	});
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
	options.Authority = "https://localhost:5001";
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
	{
		ValidateAudience = false
	};
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ApiScope", policy =>
	{
		policy.RequireAuthenticatedUser();
		policy.RequireClaim("scope", "api1");
	});
});
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.OAuth2,

		Flows = new OpenApiOAuthFlows()
		{

			AuthorizationCode = new OpenApiOAuthFlow
			{
				AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
				TokenUrl = new Uri("https://localhost:5001/connect/token"),
				Scopes = new Dictionary<string, string>
				{
					{"api1", "Demo - full access" }
				}
			}
		}
	});
	c.OperationFilter<AuthorizeCheckOperationFilter>();
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "EndTrip API", Version = "v1" });
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));

//builder.Services.AddShared();
//builder.Services.AddApplication();
builder.Services.AddInfrastructure(/*builder.Configuration*/);
builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.OAuthClientId("swagger");
		c.OAuth2RedirectUrl("https://localhost:7137/swagger/oauth2-redirect.html");
		c.OAuthUsePkce();
	});
}

app.UseHttpsRedirection();
//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(
//		   Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
//	RequestPath = "/StaticFiles"
//});
app.UseSerilogRequestLogging();
app.UseCors("MyOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
