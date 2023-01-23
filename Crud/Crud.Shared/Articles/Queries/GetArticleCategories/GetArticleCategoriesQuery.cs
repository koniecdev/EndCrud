namespace Crud.Shared.Articles.Queries;
public class GetArticleCategoriesQuery : IRequest<GetArticleCategoriesVm>
{
	public GetArticleCategoriesQuery()
	{
	}
	public GetArticleCategoriesQuery(int id)
	{
		Id = id;
	}
	public int Id { get; set; }
}