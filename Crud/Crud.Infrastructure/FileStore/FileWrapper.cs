using Crud.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Crud.Infrastructure.FileStore;

public class FileWrapper : IFileWrapper
{
	public async Task SaveFile(IFormFile formFile, Stream stream)
	{
		await formFile.CopyToAsync(stream);
	}
}
