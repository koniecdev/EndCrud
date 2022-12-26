namespace Crud.Shared.Articles.Queries;
public class GetAllArticlesVm
{
	public GetAllArticlesVm()
	{
		Articles = new List<GetAllArticlesDto>();
	}
	public ICollection<GetAllArticlesDto> Articles { get; set; }
}