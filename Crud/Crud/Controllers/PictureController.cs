using Crud.Shared.Pictures.Commands;
using Crud.Shared.Pictures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers;

[Route("api/pictures")]
public class PictureController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetAllPicturesVm>> Picture()
	{
		return await Mediator.Send(new GetAllPicturesQuery());
	}

	[HttpGet("Image/{imagePath}")]
	[AllowAnonymous]
	public async Task<ActionResult> Picture(string imagePath)
	{
		var returnedPicture = await Mediator.Send(new GetPictureQuery(imagePath));
		if (returnedPicture.Picture.FileStream == null)
		{
			return BadRequest("Picture couldnt be handled");
		}
		return File(returnedPicture.Picture.FileStream, returnedPicture.Picture.ContentType);
	}

	[HttpPost]
	public async Task<ActionResult> Picture([FromForm] IFormFile[] files)
	{
		CreatePicturesCommand command = new();
		foreach (var file in Request.Form.Files)
		{
			command.Files.Add(file);
		}
		if (command.Files == null)
		{
			return BadRequest("No picture selected!");
		}
		await Mediator.Send(command);
		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Picture(int id)
	{
		await Mediator.Send(new DeletePictureCommand(id));
		return NoContent();
	}
}