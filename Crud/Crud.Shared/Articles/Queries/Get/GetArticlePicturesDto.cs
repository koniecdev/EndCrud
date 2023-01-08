namespace Crud.Shared.Articles.Queries;
public class GetArticlePicturesDto : IMapFrom<Picture>
{
	public GetArticlePicturesDto()
	{
		RelativePath = string.Empty;
	}
	public string RelativePath { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Picture, GetArticlePicturesDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}