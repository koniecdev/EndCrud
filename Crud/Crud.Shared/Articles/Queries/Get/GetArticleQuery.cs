namespace Crud.Shared.Articles.Queries;
public class GetArticleQuery : IRequest<GetArticleVm>
{
	public int Id { get; set; }
}