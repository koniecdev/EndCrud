using Crud.Shared.Categories.Commands;

namespace Crud.Application.Categories.Commands;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
	private readonly ICrudDbContext _db;
	public DeleteCategoryCommandHandler(ICrudDbContext db)
	{
		_db = db;
	}
	public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Categories.SingleOrDefaultAsync(m=>m.Id == request.Id && m.StatusId != 0, cancellationToken);
		if (fromDb != null)
		{
			_db.Categories.Remove(fromDb);
			await _db.SaveChangesAsync(cancellationToken);
		}
		else
		{
			throw new ArgumentException("id: " + request.Id.ToString());
		}
		return Unit.Value;
	}
}