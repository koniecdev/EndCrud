namespace CrudApp;

public static class SD
{
	private readonly static string _APIUri = "https://localhost:7137/api/pictures/Image/";

	public static string APIUri
	{
		get { return _APIUri; }
	}

	private readonly static string _GoBackTo = "Go back to all ";

	public static string GoBackTo
	{
		get { return _GoBackTo; }
	}

	private readonly static string _CreateNew = "Create new ";

	public static string CreateNew
	{
		get { return _CreateNew; }
	}
}
