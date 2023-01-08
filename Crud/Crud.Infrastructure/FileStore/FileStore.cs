using Crud.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace Crud.Infrastructure.FileStore;

public class FileStore : IFileStore
{
	private readonly IFileWrapper _fileWrapper;
	private readonly IDirectoryWrapper _directoryWrapper;
	public FileStore(IFileWrapper fileWrapper, IDirectoryWrapper directoryWrapper)
	{
		_fileWrapper = fileWrapper;
		_directoryWrapper = directoryWrapper;
	}

	public async Task<string> SafeWriteFile(IFormFile formFile, string path)
	{
		_directoryWrapper.CreateDirectory(path);
		var outputFile = Path.Combine(path, formFile.FileName);
		//try
		//{
		//	if (File.Exists(outputFile))
		//	{
		//		outputFile = Path.Combine(path, string.Concat("_", formFile.FileName));
		//	}
		//}
		//catch{}
		using (var stream = new FileStream(path, FileMode.Create))
		{
			try
			{
				await formFile.CopyToAsync(stream);
				//await _fileWrapper.SaveFile(formFile, stream);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				stream.Close();
			}
		}
		
		return outputFile;
	}
}
