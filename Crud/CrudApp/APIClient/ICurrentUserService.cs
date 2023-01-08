using System.Security.Claims;

namespace CrudApp;
public interface ICurrentUserService
{
	string Id { get; set; }
	int MemberId { get; set; }
	string Email { get; set; }
	List<Claim> Roles { get; set; }
	bool IsAuthenticated { get; set; }
}
