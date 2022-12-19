namespace Crud.Domain.Common;
public class AuditableEntity
{
	public int Id { get; set; }
	public DateTime Created { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime? Modified { get; set; }
	public string? ModifiedBy { get; set; }
	public int StatusId { get; set; } = 1;
	public DateTime? Inactivated { get; set; }
	public string? InactivatedBy { get; set; }
}