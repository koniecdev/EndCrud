using AutoMapper;
using Crud.Shared.Categories.Commands;
using Crud.Shared.Categories.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class CategoryController : Controller
{
	private readonly ICrudClient _client;
	private readonly IMapper _mapper;

	public CategoryController(ICrudClient client, IMapper mapper)
	{
		_client = client;
		_mapper = mapper;
	}

	public async Task<ActionResult<GetAllCategoriesVm>> Index()
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if(accessToken == null || string.IsNullOrWhiteSpace(accessToken)){
			return Unauthorized();
		}
		var response = await _client.GetAllCategories(accessToken);
		return View(model: response);
	}

	public ActionResult Create()
	{
		return View(model: new CreateCategoryCommand());
	}

	[HttpPost, ActionName("Create")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Create(CreateCategoryCommand command)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		int response = await _client.CreateCategory(command, accessToken);
		if(!(response > 0))
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
		var response = await _client.GetCategory(id, accessToken);
		return View(model: response);
	}

	[HttpPost, ActionName("Update")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Update(GetCategoryVm command)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		await _client.UpdateCategory(_mapper.Map<UpdateCategoryCommand>(command.Category), accessToken);
		return RedirectToAction(nameof(Index));
	}
	public async Task<ActionResult> Delete(int id)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		var response = await _client.GetCategory(id, accessToken);
		return View(model: response);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Delete(GetCategoryVm command)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		await _client.DeleteCategory(command.Category.Id, accessToken);
		return RedirectToAction(nameof(Index));
	}
}
