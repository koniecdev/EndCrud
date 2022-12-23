namespace Crud.Shared.Categories.Commands;
public class UpdateCategoryCommand : IMapFrom<Category>, IRequest<Unit>
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateCategoryCommand, Category>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
		profile.CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
		profile.CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
	}
}