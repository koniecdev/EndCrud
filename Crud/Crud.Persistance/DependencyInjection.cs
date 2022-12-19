using Microsoft.Extensions.DependencyInjection;

namespace Crud.Persistance;
public static class DependencyInjection
{
	public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<CrudDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CrudDatabase")));
		services.AddScoped<ICrudDbContext, CrudDbContext>();
		return services;
	}
}