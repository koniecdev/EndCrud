namespace Crud.Shared.Categories.Commands;
public class DeleteCategoryCommand : IMapFrom<Category>, IRequest<Unit>
{
	public DeleteCategoryCommand()
	{
		Name = string.Empty;
	}
	public DeleteCategoryCommand(int id)
	{
		Id = id;
		Name = string.Empty;
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<DeleteCategoryCommand, Category>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}