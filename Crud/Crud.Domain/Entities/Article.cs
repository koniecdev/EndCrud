using Crud.Domain.Common;
namespace Crud.Domain.Entities;
public class Article : AuditableEntity
{
	public Article()
	{
		Header = string.Empty;
		Content = string.Empty;
		Member = new();
		Category = new();
	}
	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public virtual Member Member { get; set; }
	public int CategoryId { get; set; }
	public virtual Category Category { get; set; }
}
