namespace Crud.Shared.Pictures.Queries;
public class GetPictureVm
{
	public GetPictureVm()
	{
		Picture = new();
	}
	public GetPictureDto Picture { get; set; }
}