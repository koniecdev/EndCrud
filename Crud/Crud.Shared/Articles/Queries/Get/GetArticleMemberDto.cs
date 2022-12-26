namespace Crud.Shared.Articles.Queries;
public class GetArticleMemberDto : IMapFrom<Member>
{
	public GetArticleMemberDto()
	{
		UserId = string.Empty;
		Username = string.Empty;
	}
	public int Id { get; set; }
	public string UserId { get; set; }
	public string Username { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Member, GetArticleMemberDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}