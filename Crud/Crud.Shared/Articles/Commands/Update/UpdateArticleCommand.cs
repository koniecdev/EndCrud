namespace Crud.Shared.Articles.Commands;
public class UpdateArticleCommand : IMapFrom<Article>, IRequest<Unit>
{
	public UpdateArticleCommand()
	{
		Header = string.Empty;
		Content = string.Empty;
		Gallery = new List<int>();
	}
	public int Id { get; set; }
	public string Header { get; set; }
	public string Content { get; set; }
	public int CategoryId { get; set; }
	public int ThumbnailId { get; set; }
	public ICollection<int> Gallery { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateArticleCommand, Article>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}