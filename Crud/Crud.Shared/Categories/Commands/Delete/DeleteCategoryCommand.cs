namespace Crud.Shared.Categories.Commands;
public class DeleteCategoryCommand : IRequest<Unit>
{
	public int Id { get; set; }
}