namespace Crud.Shared.Categories.Queries;
public class GetCategoryQuery : IRequest<GetCategoryVm>
{
	public GetCategoryQuery(int id)
	{
		Id = id;
	}
	public int Id { get; set; }
}