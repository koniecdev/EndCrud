namespace Crud.Shared.Articles.Queries;
public class GetArticleQuery : IRequest<GetArticleVm>
{
	public GetArticleQuery()
	{

	}
	public GetArticleQuery(int id)
	{
		Id = id;
	}
	public int Id { get; set; }
}