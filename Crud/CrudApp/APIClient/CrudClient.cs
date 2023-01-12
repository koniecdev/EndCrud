﻿global using Newtonsoft.Json;
using Crud.Shared.Articles.Commands;
using Crud.Shared.Articles.Queries;
using Crud.Shared.Categories.Commands;
using Crud.Shared.Categories.Queries;
using Crud.Shared.Pictures.Commands;
using Crud.Shared.Pictures.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Text;

namespace CrudApp;

public partial class CrudClient : ICrudClient
{
	private string _baseUrl = "https://localhost:7137";
	private readonly HttpClient _httpClient;
	private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;
	private readonly ICurrentUserService _currentUserService;

	public CrudClient(IHttpClientFactory factory, ICurrentUserService currentUserService)
	{
		_httpClient = factory.CreateClient("CrudClient");
		_baseUrl = _httpClient.BaseAddress.ToString()+"api/";
		_settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
		{
			var settings = new JsonSerializerSettings();
			UpdateJsonSerializerSettings(settings);
			return settings;
		});
		_currentUserService = currentUserService;
	}

	protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

	partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);

	public string BaseUrl
	{
		get { return _baseUrl; }
		set { _baseUrl = value; }
	}

	private async Task<T> GetTask<T>(StringBuilder urlBuilder, string accessToken)
	{
		var client = _httpClient;
		try
		{
			using (var request = new HttpRequestMessage())
			{
				request.Method = new HttpMethod("GET");
				var url = urlBuilder.ToString();
				request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

	private async Task<int> CreateTask<T>(T command, string pth, string accessToken)
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
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

	private async Task UpdateTask<T>(T command, string pth, string accessToken)
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
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

	private async Task DeleteTask(int idToDelete, string pth, string accessToken)
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
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

	public async Task CreatePictures(CreatePicturesCommand command, string accessToken)
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
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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

	public async Task<GetAllPicturesVm> GetAllPictures(string accessToken)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("pictures");
		return await GetTask<GetAllPicturesVm>(urlBuilder, accessToken);
	}

	public async Task DeletePicture(int id, string accessToken)
	{
		await DeleteTask(id, "pictures", accessToken);
	}

	public async Task<GetAllCategoriesVm> GetAllCategories(string accessToken)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("categories");
		return await GetTask<GetAllCategoriesVm>(urlBuilder, accessToken);
	}

	public async Task<GetCategoryVm> GetCategory(int id, string accessToken)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("categories").Append(string.Concat("/", id.ToString()));
		return await GetTask<GetCategoryVm>(urlBuilder, accessToken);
	}

	public async Task<int> CreateCategory(CreateCategoryCommand command, string accessToken)
	{
		return await CreateTask(command, "categories", accessToken);
	}

	public async Task UpdateCategory(UpdateCategoryCommand command, string accessToken)
	{
		await UpdateTask(command, "categories", accessToken);
	}

	public async Task DeleteCategory(int id, string accessToken)
	{
		await DeleteTask(id, "categories", accessToken);
	}

	public async Task<GetAllArticlesVm> GetAllArticles(string accessToken)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("articles");
		return await GetTask<GetAllArticlesVm>(urlBuilder, accessToken);
	}

	public async Task<GetArticleVm> GetArticle(int id, string accessToken)
	{
		var urlBuilder = new StringBuilder();
		urlBuilder.Append(BaseUrl).Append("articles").Append($"/{id.ToString()}");
		return await GetTask<GetArticleVm>(urlBuilder, accessToken);
	}

	public async Task<int> CreateArticle(CreateArticleCommand command, string accessToken)
	{
		return await CreateTask(command, "articles", accessToken);
	}

	public async Task UpdateArticle(UpdateArticleCommand command, string accessToken)
	{
		await UpdateTask(command, "articles", accessToken);
	}

	public async Task DeleteArticle(int id, string accessToken)
	{
		await DeleteTask(id, "articles", accessToken);
	}
}
