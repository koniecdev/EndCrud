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
		var fromDb = await _db.Articles.Include(m=>m.Thumbnail).Include(m=>m.Pictures)
			.SingleOrDefaultAsync(m => m.Id == request.Id && m.StatusId != 0, cancellationToken);
		if(fromDb == null)
		{
			throw new ArgumentException("id: " + request.Id.ToString());
		}
		_mapper.Map(request, fromDb);
		if (request.Gallery.Count > 0)
		{
			fromDb.Pictures = new List<Picture>();
			foreach (var id in request.Gallery)
			{
				var photo = await _db.Pictures.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
				if (photo != null)
				{
					fromDb.Pictures.Add(photo);
				}
			}
		}
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}