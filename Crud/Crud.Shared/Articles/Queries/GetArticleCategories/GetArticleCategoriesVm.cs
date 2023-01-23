namespace Crud.Shared.Articles.Queries;
public class GetArticleCategoriesVm
{
	public GetArticleCategoriesVm()
	{
		Article = new();
		Categories = new List<GetArticleCategoriesCategoryDto>();
	}
	public GetArticleCategoriesArticleDto Article { get; set; }
	public ICollection<GetArticleCategoriesCategoryDto> Categories { get; set; }
}