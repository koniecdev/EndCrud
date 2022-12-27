using Crud.Domain.Common;
namespace Crud.Domain.Entities;
public class Picture : AuditableEntity
{
	public Picture()
	{
		RelativePath = string.Empty;
	}
	public string RelativePath { get; set; }
	public virtual ICollection<Article>? Thumbnails { get; set; }
	public virtual ICollection<Article>? Articles { get; set; }
}
