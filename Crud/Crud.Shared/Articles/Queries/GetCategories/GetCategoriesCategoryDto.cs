namespace Crud.Shared.Articles.Queries;
public class GetCategoriesCategoryDto : IMapFrom<Category>
{
	public GetCategoriesCategoryDto()
	{
		Name = string.Empty;
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Category, GetCategoriesCategoryDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}