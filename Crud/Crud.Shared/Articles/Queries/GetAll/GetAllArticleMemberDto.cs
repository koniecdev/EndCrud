namespace Crud.Shared.Articles.Queries;
public class GetAllArticleMemberDto : IMapFrom<Member>
{
	public GetAllArticleMemberDto()
	{
		UserId = string.Empty;
		Username = string.Empty;
	}
	public string UserId { get; set; }
	public string Username { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Member, GetAllArticleMemberDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}