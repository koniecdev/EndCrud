using Crud.Application.Common.Interfaces;

namespace Crud.Infrastructure.FileStore;

public class DirectoryWrapper : IDirectoryWrapper
{
	public void CreateDirectory(string path)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
	}
}
