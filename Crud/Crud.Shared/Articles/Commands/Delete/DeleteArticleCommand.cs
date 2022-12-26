namespace Crud.Shared.Articles.Commands;
public class DeleteArticleCommand : IRequest<Unit>
{
	public int Id { get; set; }
}