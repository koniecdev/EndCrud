namespace Crud.Shared.Pictures.Queries;
public class GetPictureQuery : IRequest<GetPictureVm>
{
	public GetPictureQuery(string imagePath)
	{
		ImagePath = imagePath;
	}
	public string ImagePath { get; set; }
}