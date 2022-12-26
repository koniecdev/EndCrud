namespace Crud.Shared.Articles.Queries;
public class GetAllArticlesDto : IMapFrom<Article>
{
	public GetAllArticlesDto()
	{
		Header = string.Empty;
		Content = string.Empty;
	}
	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public virtual Member? Member { get; set; }
	public int CategoryId { get; set; }
	public virtual Category? Category { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Article, GetAllArticlesDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}