//namespace Crud.Shared.Articles.Queries;
//public class GetCategoriesArticlePicturesDto : IMapFrom<Picture>
//{
//	public GetCategoriesArticlePicturesDto()
//	{
//		RelativePath = string.Empty;
//	}
//	public string RelativePath { get; set; }
//	public void Mapping(Profile profile)
//	{
//		profile.CreateMap<Picture, GetCategoriesArticlePicturesDto>()
//			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
//	}
//}