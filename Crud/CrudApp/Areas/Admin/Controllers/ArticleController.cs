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
	private readonly ICurrentUserService _user;
	private readonly IMapper _mapper;

	public ArticleController(ICrudClient client, IMapper mapper, ICurrentUserService user)
	{
		_client = client;
		_mapper = mapper;
		_user = user;
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

	public async Task<ActionResult<GetCategoriesVm>> Create()
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		ViewBag.access_token = accessToken;
		var response = await _client.GetArticleCategories(accessToken);
		return View(model: response);
	}

	[HttpPost, ActionName("Create")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Create(GetCategoriesVm vm)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		var command = _mapper.Map<CreateArticleCommand>(vm.Article);
		if(command == null)
		{
			return RedirectToAction(nameof(Create));
		}
		var galleryPics = Request.Form["galleryPics"];
		var galleryPicsArray = galleryPics.ToString().Split(',');
		foreach (var picId in galleryPicsArray)
		{
			command.Gallery.Add(Convert.ToInt32(picId));
		}
		command.UserId = _user.Id;
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
		return View(model: response);
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
