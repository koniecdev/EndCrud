namespace Crud.Shared.Pictures.Queries;
public class GetAllPicturesDto : IMapFrom<Picture>
{
	public GetAllPicturesDto()
	{
		RelativePath = string.Empty;
	}
	public string RelativePath { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Picture, GetAllPicturesDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}