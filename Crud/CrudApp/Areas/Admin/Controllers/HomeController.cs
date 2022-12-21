using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

}
