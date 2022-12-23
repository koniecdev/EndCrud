namespace Crud.Shared.Categories.Queries;
public class GetAllCategoriesDto : IMapFrom<Category>
{
	public GetAllCategoriesDto()
	{
		Name = string.Empty;
	}
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Category, GetAllCategoriesDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}