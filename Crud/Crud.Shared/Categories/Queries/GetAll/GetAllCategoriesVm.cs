namespace Crud.Shared.Categories.Queries;
public class GetAllCategoriesVm
{
	public GetAllCategoriesVm()
	{
		Categories = new List<GetAllCategoriesDto>();
	}
	public ICollection<GetAllCategoriesDto> Categories { get; set; }
}