using Crud.Domain.Common;
namespace Crud.Domain.Entities;
public class Article : AuditableEntity
{
	public Article()
	{
		Header = string.Empty;
		Content = string.Empty;
	}
	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public virtual Member? Member { get; set; }
	public int CategoryId { get; set; }
	public virtual Category? Category { get; set; }
	public int PictureId { get; set; }
	public virtual Picture? Picture { get; set; }
	public virtual ICollection<Picture>? Pictures { get; set; }
}
