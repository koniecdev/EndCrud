using AutoMapper;
using Crud.Shared.Articles.Commands;
using Crud.Shared.Articles.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ArticleController : Controller
{
	private readonly ICrudClient _client;
	private readonly IMapper _mapper;

	public ArticleController(ICrudClient client, IMapper mapper)
	{
		_client = client;
		_mapper = mapper;
	}

	public async Task<ActionResult<GetAllArticlesVm>> Index()
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		var response = await _client.GetAllArticles(accessToken);
		return View(model: response);
	}

	public ActionResult Create()
	{
		return View(model: new CreateArticleCommand());
	}

	[HttpPost, ActionName("Create")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Create(CreateArticleCommand command)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		int response = await _client.CreateArticle(command, accessToken);
		if (!(response > 0))
		{
			return BadRequest();
		}
		return RedirectToAction(nameof(Index));
	}

	public async Task<ActionResult> Update(int id)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		var response = await _client.GetArticle(id, accessToken);
		return View(model: response.Article);
	}

	[HttpPost, ActionName("Update")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Update(GetArticleVm command)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		await _client.UpdateArticle(_mapper.Map<UpdateArticleCommand>(command.Article), accessToken);
		return RedirectToAction(nameof(Index));
	}
	public async Task<ActionResult> Delete(int id)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		var response = await _client.GetArticle(id, accessToken);
		return View(model: response);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Delete(GetArticleVm command)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		await _client.DeleteArticle(command.Article.Id, accessToken);
		return RedirectToAction(nameof(Index));
	}
}
