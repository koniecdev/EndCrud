namespace Crud.Shared.Articles.Queries;
public class GetArticleCategoryDto : IMapFrom<Category>
{
	public GetArticleCategoryDto()
	{
		Name = string.Empty;
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Category, GetArticleCategoryDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}