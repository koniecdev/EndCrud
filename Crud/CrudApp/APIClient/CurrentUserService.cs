using IdentityModel;
using System.Security.Claims;

namespace CrudApp;

public class CurrentUserService : ICurrentUserService
{
	public int MemberId { get; set; }
	public string Id { get; set; } = "";
	public string Email { get; set; } = "";
	public List<Claim> Roles { get; set; } = new();
	public bool IsAuthenticated { get; set; }
	public CurrentUserService()
    {
    }
	public CurrentUserService(IHttpContextAccessor accessor)
	{
		if (accessor != null)
		{
			var id = accessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id);
			var email = accessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Email);
			var roles = accessor.HttpContext?.User?.FindAll(JwtClaimTypes.Role);

			Email = email;
			Id = id;
			Roles = roles.ToList();
			IsAuthenticated = !string.IsNullOrEmpty(Email);
		}
	}
}
