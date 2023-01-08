namespace Crud.Shared.Pictures.Commands;
public class DeletePictureCommand : IRequest<Unit>
{
	public int Id { get; set; }
}