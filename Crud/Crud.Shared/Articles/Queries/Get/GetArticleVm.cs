namespace Crud.Shared.Articles.Queries;
public class GetArticleVm : IMapFrom<Article>
{
	public GetArticleVm()
	{
		Article = new();
	}
	public GetArticleVm(int id)
	{
		Article = new(id);
	}
	public GetArticleDto Article { get; set; }
}