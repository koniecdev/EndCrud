namespace Crud.Shared.Members.Commands;
public class CreateMemberCommand : IMapFrom<Member>, IRequest<int>
{
	public CreateMemberCommand()
	{
		Username = string.Empty;
		UserId = string.Empty;
		Email = string.Empty;
	}
	public string UserId { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateMemberCommand, Member>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}