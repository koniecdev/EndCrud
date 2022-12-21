using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
public class ArticleController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

}
