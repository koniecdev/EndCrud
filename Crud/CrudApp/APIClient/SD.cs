namespace CrudApp;

public static class SD
{
	private readonly static string _APIUri = "https://localhost:7137/api/pictures/Image/";

	public static string APIUri
	{
		get { return _APIUri; }
	}
}
