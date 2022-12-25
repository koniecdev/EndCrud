using Crud.Shared.Categories.Commands;

namespace Crud.Application.Categories.Commands;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public UpdateCategoryCommandHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Categories.SingleOrDefaultAsync(m => m.Id == request.Id && m.StatusId != 0, cancellationToken);
		if(fromDb == null)
		{
			throw new ArgumentException("id: " + request.Id.ToString());
		}
		_mapper.Map(request, fromDb);
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}