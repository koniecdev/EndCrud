namespace Crud.Shared.Articles.Queries;
public class GetArticleCategoriesCategoryDto : IMapFrom<Category>
{
	public GetArticleCategoriesCategoryDto()
	{
		Name = string.Empty;
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Category, GetArticleCategoriesCategoryDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}