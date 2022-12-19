namespace Crud.Application.Common.Interfaces;
public interface IFileStore
{
	public string SafeWriteFile(byte[] content, string sourceFileName, string path);
}
