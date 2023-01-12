namespace Crud.Shared.Articles.Commands;
public class CreateArticleCommand : IMapFrom<Article>, IRequest<int>
{
	public CreateArticleCommand()
	{
		Header = string.Empty;
		Content = string.Empty;
	}

	public string Header { get; set; }
	public string Content { get; set; }
	public int MemberId { get; set; }
	public int CategoryId { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateArticleCommand, Article>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}