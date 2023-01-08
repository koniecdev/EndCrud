namespace Crud.Shared.Articles.Queries;
public class GetArticleVm : IMapFrom<Article>
{
	public GetArticleVm()
	{
		Header = string.Empty;
		Content = string.Empty;
	}
	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public virtual GetArticleMemberDto? Member { get; set; }
	public int CategoryId { get; set; }
	public virtual GetArticleCategoryDto? Category { get; set; }
	public virtual GetArticlePictureDto? Thumbnail { get; set; }
	public virtual GetArticlePicturesDto? Pictures { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Article, GetArticleVm>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}