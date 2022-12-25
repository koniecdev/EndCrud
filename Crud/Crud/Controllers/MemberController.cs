using Crud.Shared.Members.Commands;
using Crud.Shared.Members.Queries;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crud.API.Controllers;

[Route("api/members")]
public class MemberController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetAllMembersVm>> GetAllMembers()
	{
		var response = await Mediator.Send(new GetAllMembersQuery());
		return Ok(response);
	}

	[HttpPost]
	[AllowAnonymous]
	public async Task<ActionResult<int>> Member(CreateMemberCommand command)
	{
		var secret = Request.Headers["AuthEnd"].ToString();
		if ("secret".ToSha512() != secret)
		{
			return Conflict();
		}
		try
		{
			int response = await Mediator.Send(command);
			return Ok(response);
		}
		catch
		{
			return UnprocessableEntity();
		}
	}

	[HttpPatch]
	[AllowAnonymous]
	public async Task<ActionResult> Member(UpdateMemberCommand command)
	{
		var secret = Request.Headers["AuthEnd"].ToString();
		if ("secret".ToSha512() != secret)
		{
			return Conflict();
		}
		try
		{
			await Mediator.Send(command);
			return NoContent();
		}
		catch
		{
			return UnprocessableEntity();
		}
	}

	[HttpDelete("{userId}")]
	[AllowAnonymous]
	public async Task<ActionResult> Member(string userId)
	{
		var secret = Request.Headers["AuthEnd"].ToString();
		if ("secret".ToSha512() != secret)
		{
			return Conflict();
		}
		try
		{
			await Mediator.Send(new DeleteMemberCommand() { UserId = userId });
			return NoContent();
		}
		catch
		{
			return UnprocessableEntity();
		}
	}
}
