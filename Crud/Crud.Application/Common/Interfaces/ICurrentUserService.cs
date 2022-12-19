namespace Crud.Application.Common.Interfaces;
public interface ICurrentUserService
{
	string Id { get; set; }
	string Email { get; set; }
	bool IsAuthenticated { get; set; }
}
