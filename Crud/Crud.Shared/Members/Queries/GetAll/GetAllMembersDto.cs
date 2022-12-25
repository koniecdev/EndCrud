namespace Crud.Shared.Members.Queries;
public class GetAllMembersDto : IMapFrom<Member>
{
	public GetAllMembersDto()
	{
		UserId = string.Empty;
		Username = string.Empty;
		Email = string.Empty;
	}
	public string UserId { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Member, GetAllMembersDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}