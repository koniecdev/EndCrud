using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Text;

namespace CrudApp.APIClient;
public class TokenAuthorization : ITokenAuthorization
{
	private readonly HttpContext context;
	private string _IS4baseUrl = "https://localhost:5001";
	private readonly HttpClient _IS4httpClient;
	public string IS4BaseUrl
	{
		get { return _IS4baseUrl; }
		set { _IS4baseUrl = value; }
	}
	public TokenAuthorization(IHttpContextAccessor accessor, IHttpClientFactory factory)
	{
		context = accessor.HttpContext;
		_IS4httpClient = factory.CreateClient("IS4Client");
		_IS4baseUrl = _IS4httpClient?.BaseAddress?.ToString() + "";
	}

	public async Task<string> GetToken()
	{
		var accessToken = await context.GetTokenAsync("access_token");

		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			var refreshToken = await context.GetTokenAsync("refresh_token");
			await RefreshToken(refreshToken);
			accessToken = await context.GetTokenAsync("access_token");
		}
		return accessToken;
	}

	public async Task RefreshToken(string refreshToken)
	{
		var client = _IS4httpClient;
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("GET");
				var urlBuilder = new StringBuilder();
				urlBuilder.Append(IS4BaseUrl).Append("connect/token");
				var url = urlBuilder.ToString();
				//request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

				var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
				{
					Address = url,

					ClientId = "mvc",
					ClientSecret = "secret",

					RefreshToken = refreshToken
				});
				var auth = await context.AuthenticateAsync("Cookies");
				auth.Properties.StoreTokens(new List<AuthenticationToken>()
				{
					new AuthenticationToken()
					{
						Name = OpenIdConnectParameterNames.AccessToken,
						Value = response.AccessToken
					},
					new AuthenticationToken()
					{
						Name = OpenIdConnectParameterNames.RefreshToken,
						Value = response.RefreshToken
					}
				});

				await context.SignInAsync(auth.Principal, auth.Properties);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

}
