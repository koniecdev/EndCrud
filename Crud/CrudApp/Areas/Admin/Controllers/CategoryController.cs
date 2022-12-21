using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

}
