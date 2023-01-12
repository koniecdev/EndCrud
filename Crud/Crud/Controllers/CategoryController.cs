using Crud.Shared.Categories.Commands;
using Crud.Shared.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers;

[Route("api/categories")]
public class CategoryController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetAllCategoriesVm>> GetAll()
	{
		var response = await Mediator.Send(new GetAllCategoriesQuery());
		if (response == null)
		{
			return BadRequest();
		}
		return Ok(response);
	}
	[HttpGet("GetById/{id}")]
	public async Task<ActionResult<GetCategoryVm>> Get(int id)
	{
		if (!(id > 0 && id < int.MaxValue))
		{
			return BadRequest();
		}
		var response = await Mediator.Send(new GetCategoryQuery(id));
		if (response == null)
		{
			return NotFound();
		}
		return Ok(response);
	}
	[HttpPost]
	public async Task<ActionResult<int>> Category(CreateCategoryCommand command)
	{
		if (command is null)
		{
			return BadRequest();
		}

		if (string.IsNullOrWhiteSpace(command.Name))
		{
			return ValidationProblem();
		}
		var response = await Mediator.Send(command);
		if (response == 0)
		{
			return BadRequest();
		}
		return Ok(response);
	}
	[HttpPatch]
	public async Task<ActionResult> Category(UpdateCategoryCommand command)
	{
		if (command is null)
		{
			return BadRequest();
		}
		try
		{
			await Mediator.Send(command);
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}

		return NoContent();
	}
	[HttpDelete("{id}")]
	public async Task<ActionResult> Category(int id)
	{
		if (id > 0 && id < int.MaxValue)
		{
			try
			{
				await Mediator.Send(new DeleteCategoryCommand(id));
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
			return NoContent();
		}
		return BadRequest("Invalid parameter range");
	}
}