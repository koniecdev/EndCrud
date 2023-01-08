using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Crud.Shared;
public static class DependencyInjection
{
	public static IServiceCollection AddShared(this IServiceCollection services)
	{
		//services.AddScoped<IFormFile, MyFormFile>();
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		return services;
	}
}

