using Crud.Shared.Members.Queries;
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
	public async Task<ActionResult<GetAllMembersVm>> Index()
	{
		var response = await _client.GetAllMembers();
		return View(model: response);
	}

}
