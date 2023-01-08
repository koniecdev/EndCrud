namespace Crud.Shared.Pictures.Queries;
public class GetPictureDto : IMapFrom<Picture>
{
	public GetPictureDto()
	{
		RelativePath = string.Empty;
		ContentType = string.Empty;
	}
	public int Id { get; set; }
	public string RelativePath { get; set; }
	public byte[]? FileStream { get; set; }
	public string ContentType { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Picture, GetPictureDto>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
	}
}