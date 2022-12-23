using Crud.Shared.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers;

[Route("api/categories")]
public class CategoryController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetAllCategoriesVm>> GetAll()
	{
		var response = await Mediator.Send(new GetAllCategoriesQuery());
		if(response == null)
		{
			return BadRequest();
		}
		return Ok(response);
	}
}