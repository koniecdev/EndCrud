namespace Crud.Shared.Categories.Commands;
public class UpdateCategoryCommand : IMapFrom<Category>, IRequest<Unit>
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateCategoryCommand, Category>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}