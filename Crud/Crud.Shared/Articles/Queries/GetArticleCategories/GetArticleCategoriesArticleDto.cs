using Crud.Shared.Articles.Commands;

namespace Crud.Shared.Articles.Queries;
public class GetArticleCategoriesArticleDto : IMapFrom<Article>
{
	public GetArticleCategoriesArticleDto()
	{
		Header = string.Empty;
		Content = string.Empty;
		GalleryString = string.Empty;
	}
	public int Id { get; set; }
	public string Header { get; set; }
	public string Content { get; set; }
	public int CategoryId { get; set; }
	public int ThumbnailId { get; set; }
	public string GalleryString { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Article, GetArticleCategoriesArticleDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
		profile.CreateMap<GetArticleCategoriesArticleDto, UpdateArticleCommand>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}