using Microsoft.AspNetCore.Http;

namespace Crud.Application.Common.Interfaces;
public interface IFileWrapper
{
	Task SaveFile(IFormFile formFile, Stream stream);
}
