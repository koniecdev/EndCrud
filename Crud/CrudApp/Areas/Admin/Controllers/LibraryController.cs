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
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		return View(model: await _client.GetAllPictures(accessToken));

	}

	public IActionResult Create()
	{
		CreatePicturesCommand command = new();
		return View(command);
	}

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
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		await _client.CreatePictures(command, accessToken);
		return RedirectToAction(nameof(Index));
	}

	public IActionResult Delete(int id, string imgPath)
	{
		Tuple<int, string> tuple = new(id, imgPath);
		return View(model: tuple);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> DeletePost(int id)
	{
		var accessToken = await HttpContext.GetTokenAsync("access_token");
		if (accessToken == null || string.IsNullOrWhiteSpace(accessToken))
		{
			return Unauthorized();
		}
		await _client.DeletePicture(id, accessToken);
		return RedirectToAction(nameof(Index));
	}
}
