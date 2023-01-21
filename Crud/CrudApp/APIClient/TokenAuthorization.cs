using Microsoft.AspNetCore.Authentication;

namespace CrudApp.APIClient;
public class TokenAuthorization
{
	private readonly ICrudClient _client;
	private readonly HttpContext context;
	public TokenAuthorization(IHttpContextAccessor accessor, ICrudClient client)
	{
		context = accessor.HttpContext;
		_client = client;
	}

	public async Task<string> TokenAuth()
	{
		var accessToken = await context.GetTokenAsync("access_token");

		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			var refreshToken = await context.GetTokenAsync("refresh_token");
			await _client.RefreshToken(refreshToken);
			accessToken = await context.GetTokenAsync("access_token");
		}
		return accessToken;
	}

}
