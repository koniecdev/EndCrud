using Crud.Shared.Articles.Commands;

namespace Crud.Application.Articles.Commands;
public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
{
	private readonly ICrudDbContext _db;
	public DeleteArticleCommandHandler(ICrudDbContext db)
	{
		_db = db;
	}
	public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Articles.SingleOrDefaultAsync(m=>m.Id == request.Id && m.StatusId != 0, cancellationToken);
		if (fromDb != null)
		{
			_db.Articles.Remove(fromDb);
			await _db.SaveChangesAsync(cancellationToken);
		}
		else
		{
			throw new ArgumentException("id: " + request.Id.ToString());
		}
		return Unit.Value;
	}
}