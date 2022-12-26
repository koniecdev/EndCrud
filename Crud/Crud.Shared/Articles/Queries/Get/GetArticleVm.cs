namespace Crud.Shared.Articles.Queries;
public class GetArticleVm : IMapFrom<Article>
{
	public GetArticleVm()
	{
		Header = string.Empty;
		Content = string.Empty;
		Member = new();
		Category = new();
	}
	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public GetArticleMemberDto Member { get; set; }
	public int CategoryId { get; set; }
	public GetArticleCategoryDto Category { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Article, GetArticleVm>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}