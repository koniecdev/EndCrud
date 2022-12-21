using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
public class MemberController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

}
