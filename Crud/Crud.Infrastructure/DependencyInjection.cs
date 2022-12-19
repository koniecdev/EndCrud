using Crud.Application.Common.Interfaces;
using Crud.Infrastructure.FileStore;
using Crud.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crud.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services/*, IConfiguration configuration*/)
	{
		services.AddTransient<IDateTime, DateTimeService>();
		services.AddTransient<IFileWrapper, FileWrapper>();
		services.AddTransient<IDirectoryWrapper, DirectoryWrapper>();
		services.AddTransient<IFileStore, FileStore.FileStore>();
		return services;
	}
}