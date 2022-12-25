using Crud.Shared.Members.Commands;

namespace Application.Members.Commands;
public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Unit>
{
	private readonly ICrudDbContext _db;
	public DeleteMemberCommandHandler(ICrudDbContext db)
	{
		_db = db;
	}
	public async Task<Unit> Handle(DeleteMemberCommand command, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Members.SingleOrDefaultAsync(m=>m.UserId == command.UserId, cancellationToken);
		if(fromDb == null)
		{
			throw new NotFoundException(command.UserId);
		}
		_db.Members.Remove(fromDb);
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
