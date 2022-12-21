namespace Crud.Application.Common.Exceptions;

public class MappingException : Exception
{
	public MappingException(string dtoName, Exception ex)
		: base($"There seems to be a problem with mapping of: {dtoName}.", ex)
	{
	}
}
