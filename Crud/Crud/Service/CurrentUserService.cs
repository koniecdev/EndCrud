using Crud.Application.Common.Interfaces;
using IdentityModel;
using System.Security.Claims;

namespace Crud.Service;
public class CurrentUserService : ICurrentUserService
{
	public string Id { get; set; } = "";
	public string Email { get; set; } = "";
	public List<Claim> Roles { get; set; } = new();
	public bool IsAuthenticated { get; set; }

	public CurrentUserService()
	{
		Email = "DummyEmail@dummy.com";
		Id = "DummyId";
		IsAuthenticated = true;
	}

	public CurrentUserService(IHttpContextAccessor accessor)
	{
		if (accessor != null)
		{
			var id = accessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id);
			var email = accessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Email);
			var roles = accessor.HttpContext?.User?.FindAll(JwtClaimTypes.Role);
			Email = string.IsNullOrEmpty(email) ? string.Empty : email;
			Id = string.IsNullOrEmpty(id) ? string.Empty : id;
			Roles = (roles == null) ? new List<Claim>() : roles.ToList();
			IsAuthenticated = !string.IsNullOrEmpty(Email);
		}
	}
}
