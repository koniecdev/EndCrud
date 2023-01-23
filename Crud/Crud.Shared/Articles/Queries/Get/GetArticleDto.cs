namespace Crud.Shared.Articles.Queries;
public class GetArticleDto : IMapFrom<Article>
{
	public GetArticleDto()
	{
		Header = string.Empty;
		Content = string.Empty;
	}
	public GetArticleDto(int id)
	{
		Id = id;
		Header = string.Empty;
		Content = string.Empty;
	}
	public int Id { get; set; }
	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public virtual GetArticleMemberDto? Member { get; set; }
	public int CategoryId { get; set; }
	public virtual GetArticleCategoryDto? Category { get; set; }
	public virtual GetArticlePictureDto? Thumbnail { get; set; }
	public virtual ICollection<GetArticlePicturesDto>? Pictures { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Article, GetArticleDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}