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
		var response = await _client.GetAllArticles();
		return View(model: response);
	} 

	public async Task<ActionResult<GetCategoriesVm>> Create()
	{
		ViewBag.access_token = await HttpContext.GetTokenAsync("access_token");
		var response = await _client.GetArticleCategories();
		return View(model: response);
	}

	[HttpPost, ActionName("Create")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Create(GetCategoriesVm vm)
	{
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
		int response = await _client.CreateArticle(command);
		if (!(response > 0))
		{
			return BadRequest();
		}
		return RedirectToAction(nameof(Index));
	}

	public async Task<ActionResult> Update(int id)
	{
		var response = await _client.GetArticle(id);
		return View(model: response);
	}

	[HttpPost, ActionName("Update")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Update(GetArticleVm command)
	{
		await _client.UpdateArticle(_mapper.Map<UpdateArticleCommand>(command.Article));
		return RedirectToAction(nameof(Index));
	}
	public async Task<ActionResult> Delete(int id)
	{
		var response = await _client.GetArticle(id);
		return View(model: response);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Delete(GetArticleVm command)
	{
		await _client.DeleteArticle(command.Article.Id);
		return RedirectToAction(nameof(Index));
	}
}
