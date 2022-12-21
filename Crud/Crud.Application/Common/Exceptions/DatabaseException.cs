namespace Crud.Application.Common.Exceptions;

public class DatabaseException : Exception
{
	public DatabaseException(string obj, Exception ex)
		: base($"There seems to be a problem with inserting/updating entity into database: {obj}.", ex)
	{
	}
}
