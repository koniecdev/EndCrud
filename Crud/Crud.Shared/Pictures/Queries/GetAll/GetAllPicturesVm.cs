namespace Crud.Shared.Pictures.Queries;
public class GetAllPicturesVm
{
	public GetAllPicturesVm()
	{
		Pictures = new List<GetAllPicturesDto>();
	}
	public ICollection<GetAllPicturesDto> Pictures { get; set; }
}