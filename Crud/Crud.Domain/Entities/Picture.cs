using Crud.Domain.Common;
namespace Crud.Domain.Entities;
public class Picture : AuditableEntity
{
	public Picture()
	{
		RelativePath = string.Empty;
	}
	public string RelativePath { get; set; }
}
