namespace Crud.Shared.Members.Commands;
public class UpdateMemberCommand : IMapFrom<Member>, IRequest<Unit>
{
	public UpdateMemberCommand()
	{
		UserId = string.Empty;
	}
	public string UserId { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateMemberCommand, Member>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}