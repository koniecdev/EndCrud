using Crud.Shared;
using Crud.Shared.Pictures.Commands;
using Crud.Shared.Pictures.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
[Authorize()]
public class LibraryController : Controller
{
	private readonly ICrudClient _client;
	public LibraryController(ICrudClient client)
	{
		_client = client;
	}
	public async Task<ActionResult<GetAllPicturesVm>> Index()
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if(accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		return View(model: await _client.GetAllPictures(accessToken));

	}

	[AllowAnonymous]
	public IActionResult Create()
	{
		CreatePicturesCommand command = new();
		return View(command);
	}

	[AllowAnonymous]
	[HttpPost, ActionName("Create")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> CreatePost(CreatePicturesCommand command)
	{
		if (Request.Form.Files.Count == 0)
		{
			return View(command);
		}

		List<IFormFile> formFiles = Request.Form.Files.ToList();

		command.Files = formFiles;
		await _client.CreatePictures(command);
		return RedirectToAction(nameof(Index));
	}
}
