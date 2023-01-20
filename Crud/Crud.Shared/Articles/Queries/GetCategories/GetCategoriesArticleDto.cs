using Crud.Shared.Articles.Commands;

namespace Crud.Shared.Articles.Queries;
public class GetCategoriesArticleDto : IMapFrom<Article>
{
	public GetCategoriesArticleDto()
	{
		Header = string.Empty;
		Content = string.Empty;
		GalleryString = string.Empty;
	}
	public string Header { get; set; }
	public string Content { get; set; }
	public int CategoryId { get; set; }
	public int ThumbnailId { get; set; }
	public string GalleryString { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Article, GetCategoriesArticleDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
		profile.CreateMap<GetCategoriesArticleDto, CreateArticleCommand>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}