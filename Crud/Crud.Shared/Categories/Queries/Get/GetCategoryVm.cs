namespace Crud.Shared.Categories.Queries;
public class GetCategoryVm
{
	public GetCategoryVm()
	{
		Category = new();
	}
	public GetCategoryDto Category { get; set; }
}