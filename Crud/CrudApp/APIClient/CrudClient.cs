global using Newtonsoft.Json;
using Crud.Shared.Articles.Commands;
using Crud.Shared.Articles.Queries;
using Crud.Shared.Categories.Commands;
using Crud.Shared.Categories.Queries;
using Crud.Shared.Members.Queries;
using Crud.Shared.Pictures.Commands;
using Crud.Shared.Pictures.Queries;
using CrudApp.APIClient;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace CrudApp;

public partial class CrudClient : ICrudClient
{
	private string _baseUrl = "https://localhost:7137";
	private readonly HttpClient _httpClient;
	private readonly HttpContext context;
	private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;
	private readonly ICurrentUserService _currentUserService;
	private readonly ITokenAuthorization _tokenService;
	private readonly string AccessToken;

	public CrudClient(IHttpClientFactory factory, ICurrentUserService currentUserService, IHttpContextAccessor accessor, ITokenAuthorization tokenService)
	{
		_httpClient = factory.CreateClient("CrudClient");
		_baseUrl = _httpClient?.BaseAddress?.ToString()+"api/";
		_settings = new Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
		{
			var settings = new JsonSerializerSettings();
			UpdateJsonSerializerSettings(settings);
			return settings;
		});
		_currentUserService = currentUserService;
		if(accessor.HttpContext != null)
		{
			context = accessor.HttpContext;
		}
		_tokenService = tokenService;
	}

	protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

	partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);

	public string BaseUrl
	{
		get { return _baseUrl; }
		set { _baseUrl = value; }
	}

	private async Task<T> GetTask<T>(StringBuilder urlBuilder)
	{
		var client = _httpClient;
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("GET");
				var url = urlBuilder.ToString();
				request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
				var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					if (responseData != null)
					{
						T model = JsonConvert.DeserializeObject<T>(responseData);
						return model;
					}
					throw new Exception("API returned error");
				}
				else
				{
					throw new Exception(response.StatusCode.ToString());
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private async Task<int> CreateTask<T>(T command, string pth)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append(pth);
		var client = _httpClient;
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("POST");
				var url = urlBuilder.ToString();
				request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				request.Content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
				var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
				{
					var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					if (responseData != null)
					{
						int newId = JsonConvert.DeserializeObject<int>(responseData);
						return newId;
					}
					throw new Exception("API returned error");
				}
				else
				{
					throw new Exception(response.StatusCode.ToString());
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private async Task UpdateTask<T>(T command, string pth)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append(pth);
		var client = _httpClient;
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("PATCH");
				var url = urlBuilder.ToString();
				request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				request.Content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
				var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

				if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
				{
					throw new Exception(response.StatusCode.ToString());
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private async Task DeleteTask(int idToDelete, string pth)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append(pth).Append('/').Append(idToDelete);
		var client = _httpClient;
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("DELETE");
				var url = urlBuilder.ToString();
				request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
				var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

				if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
				{
					throw new Exception(response.StatusCode.ToString());
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public async Task CreatePictures(CreatePicturesCommand command)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("pictures");
		var client = _httpClient;
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("POST");
				var url = urlBuilder.ToString();
				request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());

				MultipartFormDataContent form = new();
				foreach (var file in command.Files)
				{
					form.Add(new StreamContent(file.OpenReadStream()), file.FileName, file.FileName);
				}

				request.Content = form;
				request.Content.Headers.ContentType.MediaType = "multipart/form-data";

				var response = await client.SendAsync(request, CancellationToken.None).ConfigureAwait(false);

				if (!(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created))
				{
					throw new Exception(response.StatusCode.ToString());
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	

	public async Task<GetAllPicturesVm> GetAllPictures()
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("pictures");
		return await GetTask<GetAllPicturesVm>(urlBuilder);
	}

	public async Task DeletePicture(int id)
	{
		await DeleteTask(id, "pictures");
	}

	public async Task<GetAllCategoriesVm> GetAllCategories()
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("categories");
		return await GetTask<GetAllCategoriesVm>(urlBuilder);
	}

	public async Task<GetCategoryVm> GetCategory(int id)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("categories").Append(string.Concat("/", id.ToString()));
		return await GetTask<GetCategoryVm>(urlBuilder);
	}

	public async Task<int> CreateCategory(CreateCategoryCommand command)
	{
		return await CreateTask(command, "categories");
	}

	public async Task UpdateCategory(UpdateCategoryCommand command)
	{
		await UpdateTask(command, "categories");
	}

	public async Task DeleteCategory(int id)
	{
		await DeleteTask(id, "categories");
	}

	public async Task<GetAllArticlesVm> GetAllArticles()
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("articles");
		return await GetTask<GetAllArticlesVm>(urlBuilder);
	}

	public async Task<GetCategoriesVm> GetArticleCategories()
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("articles/categories");
		return await GetTask<GetCategoriesVm>(urlBuilder);
	}

	public async Task<GetArticleVm> GetArticle(int id)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("articles").Append($"/{id.ToString()}");
		return await GetTask<GetArticleVm>(urlBuilder);
	}

	public async Task<int> CreateArticle(CreateArticleCommand command)
	{
		return await CreateTask(command, "articles");
	}

	public async Task UpdateArticle(UpdateArticleCommand command)
	{
		await UpdateTask(command, "articles");
	}

	public async Task DeleteArticle(int id)
	{
		await DeleteTask(id, "articles");
	}

	public async Task<GetAllMembersVm> GetAllMembers()
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("members");
		return await GetTask<GetAllMembersVm>(urlBuilder);
	}
}
