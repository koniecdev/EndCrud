namespace Crud.Shared.Categories.Queries;
public class GetCategoryDto : IMapFrom<Category>
{
	public GetCategoryDto()
	{
		Name = string.Empty;
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Category, GetCategoryDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}