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
		var response = await _client.GetAllCategories();
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
		int response = await _client.CreateCategory(command);
		if(!(response > 0))
		{
			return BadRequest();
		}
		return RedirectToAction(nameof(Index));
	}

	public async Task<ActionResult> Update(int id)
	{
		var response = await _client.GetCategory(id);
		return View(model: response);
	}

	[HttpPost, ActionName("Update")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Update(GetCategoryVm command)
	{
		await _client.UpdateCategory(_mapper.Map<UpdateCategoryCommand>(command.Category));
		return RedirectToAction(nameof(Index));
	}
	public async Task<ActionResult> Delete(int id)
	{
		var response = await _client.GetCategory(id);
		return View(model: response);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Delete(GetCategoryVm command)
	{
		await _client.DeleteCategory(command.Category.Id);
		return RedirectToAction(nameof(Index));
	}
}
