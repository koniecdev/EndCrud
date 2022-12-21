namespace Crud.Shared.Categories.Commands;
public class CreateCategoryCommand : IMapFrom<Category>, IRequest<int>
{
	public CreateCategoryCommand()
	{
		Name = string.Empty;
	}
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateCategoryCommand, Category>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}