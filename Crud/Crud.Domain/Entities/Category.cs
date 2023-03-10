using Crud.Domain.Common;
namespace Crud.Domain.Entities;
public class Category : AuditableEntity
{
	public Category()
	{
		Name = string.Empty;
	}
	public string Name { get; set; } = "";
	public virtual ICollection<Article>? Articles { get; set; }
}
