namespace Crud.Shared.Articles.Commands;
public class DeleteArticleCommand : IRequest<Unit>
{
	public DeleteArticleCommand()
	{

	}
	public DeleteArticleCommand(int id)
	{
		Id = id;
	}
	public int Id { get; set; }
}