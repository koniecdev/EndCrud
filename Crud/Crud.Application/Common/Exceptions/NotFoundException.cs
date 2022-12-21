namespace Crud.Application.Common.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException(string id, Exception ex)
		: base($"There is no item in database with provided id: {id}.", ex)
	{
	}
}
