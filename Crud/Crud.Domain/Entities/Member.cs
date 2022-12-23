using Crud.Domain.Common;
namespace Crud.Domain.Entities;
public class Member : AuditableEntity
{
	public Member()
	{
		UserId = string.Empty;
		Username = string.Empty;
		Email = string.Empty;
	}
	public string UserId { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public ICollection<Article>? Articles { get; set; }
}
