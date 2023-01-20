using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Admin.Controllers;

[Area("Admin")]
public class MemberController : Controller
{
	private readonly ICrudClient _client;
	public MemberController(ICrudClient client)
	{
		_client = client;
	}
	public IActionResult Index()
	{
		return View();
	}

}
