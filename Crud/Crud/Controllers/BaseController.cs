using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers;

[EnableCors("MyOrigins")]
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
	private IMediator _mediator;
	protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
