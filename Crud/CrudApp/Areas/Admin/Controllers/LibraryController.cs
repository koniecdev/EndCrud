using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
public class LibraryController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

}
