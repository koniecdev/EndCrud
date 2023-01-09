namespace Crud.Shared.Pictures.Commands;
public class DeletePictureCommand : IRequest<Unit>
{
	public DeletePictureCommand(int id)
	{
		Id = id;
	}
	public int Id { get; set; }
}