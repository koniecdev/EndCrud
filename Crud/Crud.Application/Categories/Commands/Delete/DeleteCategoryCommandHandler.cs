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
		var fromDb = await _db.Categories.SingleAsync(m=>m.Id == request.Id, cancellationToken);
		if(fromDb == null)
		{
			throw new NotFoundException(request.Id.ToString(), new Exception());
		}
		_db.Categories.Remove(fromDb);
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}