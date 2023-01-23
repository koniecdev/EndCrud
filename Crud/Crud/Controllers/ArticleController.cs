using Crud.Shared.Articles.Commands;
using Crud.Shared.Articles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers;

[Route("api/articles")]
public class ArticleController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetAllArticlesVm>> Articles()
	{
		var response = await Mediator.Send(new GetAllArticlesQuery());
		if(response == null)
		{
			return BadRequest();
		}
		return Ok(response);
	}
	[HttpGet("{id}")]
	public async Task<ActionResult<GetAllArticlesVm>> GetArticle(int id)
	{
		if(!(id > 0 && id < int.MaxValue))
		{
			return BadRequest();
		}
		var response = await Mediator.Send(new GetArticleQuery(id));
		if (response == null)
		{
			return NotFound();
		}
		return Ok(response);
	}
	[HttpGet("categories")]
	public async Task<ActionResult<GetAllArticlesVm>> GetCategories()
	{
		var response = await Mediator.Send(new GetCategoriesQuery());
		if (response == null)
		{
			return NotFound();
		}
		return Ok(response);
	}
	[HttpGet("categories/{id}")]
	public async Task<ActionResult<GetAllArticlesVm>> GetCategories(int id)
	{
		if (!(id > 0 && id < int.MaxValue))
		{
			return BadRequest();
		}
		var response = await Mediator.Send(new GetArticleCategoriesQuery(id));
		if (response == null)
		{
			return NotFound();
		}
		return Ok(response);
	}
	[HttpPost]
	public async Task<ActionResult<int>> Article(CreateArticleCommand command)
	{
		if (command is null)
		{
			return BadRequest();
		}

		if (string.IsNullOrWhiteSpace(command.Header) || string.IsNullOrWhiteSpace(command.Content)
			|| command.CategoryId == 0 || string.IsNullOrWhiteSpace(command.UserId))
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
	public async Task<ActionResult> Article(UpdateArticleCommand command)
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
	public async Task<ActionResult> Article(int id)
	{
		if (id > 0 && id < int.MaxValue)
		{
			try
			{
				await Mediator.Send(new DeleteArticleCommand(id));
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