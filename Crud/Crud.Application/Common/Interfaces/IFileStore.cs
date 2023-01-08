using Microsoft.AspNetCore.Http;
namespace Crud.Application.Common.Interfaces;
public interface IFileStore
{
	Task<string> SafeWriteFile(IFormFile formFile, string path);
}
