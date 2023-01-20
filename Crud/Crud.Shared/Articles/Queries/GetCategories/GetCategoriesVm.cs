namespace Crud.Shared.Articles.Queries;
public class GetCategoriesVm
{
	public GetCategoriesVm()
	{
		Article = new();
		Categories = new List<GetCategoriesCategoryDto>();
	}
	public GetCategoriesArticleDto Article { get; set; }
	public ICollection<GetCategoriesCategoryDto> Categories { get; set; }
}