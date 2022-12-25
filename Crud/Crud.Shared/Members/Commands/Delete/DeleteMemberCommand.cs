namespace Crud.Shared.Members.Commands;
public class DeleteMemberCommand : IRequest<Unit>
{
	public DeleteMemberCommand()
	{
		UserId = string.Empty;
	}
	public string UserId { get; set; }
}