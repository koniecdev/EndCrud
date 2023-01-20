//namespace Crud.Shared.Articles.Queries;
//public class GetCategoriesArticlePictureDto : IMapFrom<Picture>
//{
//	public GetCategoriesArticlePictureDto()
//	{
//		RelativePath = string.Empty;
//	}
//	public string RelativePath { get; set; }
//	public void Mapping(Profile profile)
//	{
//		profile.CreateMap<Picture, GetCategoriesArticlePictureDto>()
//			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
//	}
//}