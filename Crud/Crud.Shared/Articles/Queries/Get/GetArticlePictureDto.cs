namespace Crud.Shared.Articles.Queries;
public class GetArticlePictureDto : IMapFrom<Picture>
{
	public GetArticlePictureDto()
	{
		RelativePath = string.Empty;
	}
	public string RelativePath { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Picture, GetArticlePictureDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}