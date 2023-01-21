namespace CrudApp.APIClient;
public interface ITokenAuthorization
{
	Task<string> GetToken();
	Task RefreshToken(string refreshToken);
}
