using Crud.Shared.Articles.Commands;

namespace Crud.Application.Articles.Commands;
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public UpdateArticleCommandHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Articles.SingleOrDefaultAsync(m => m.Id == request.Id && m.StatusId != 0, cancellationToken);
		if(fromDb == null)
		{
			throw new ArgumentException("id: " + request.Id.ToString());
		}
		_mapper.Map(request, fromDb);
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}