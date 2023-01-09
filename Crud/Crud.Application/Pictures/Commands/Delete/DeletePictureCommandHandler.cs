using Crud.Shared.Pictures.Commands;

namespace Crud.Application.Pictures.Commands;
public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand, Unit>
{
	private readonly ICrudDbContext _db;
	public DeletePictureCommandHandler(ICrudDbContext db)
	{
		_db = db;
	}
	public async Task<Unit> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Pictures.SingleOrDefaultAsync(m=>m.Id == request.Id && m.StatusId != 0, cancellationToken);
		if (fromDb != null)
		{
			_db.Pictures.Remove(fromDb);
			await _db.SaveChangesAsync(cancellationToken);
		}
		else
		{
			throw new ArgumentException("Couldnt find picture with id: " + request.Id.ToString());
		}
		return Unit.Value;
	}
}