using Crud.Shared.Articles.Queries;

namespace Crud.Application.Articles.Commands;
public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, GetArticleVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetArticleQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetArticleVm> Handle(GetArticleQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Articles.Include(m => m.Category).Include(m => m.Member)
			.SingleOrDefaultAsync(m => m.Id == query.Id && m.StatusId != 0,
			cancellationToken);
		if(fromDb == null)
		{
			throw new NotFoundException(query.Id.ToString());
		}
		var mapped = _mapper.Map<GetArticleDto>(fromDb);
		if (mapped == null)
		{
			throw new MappingException(nameof(GetArticleVm), new Exception());
		}
		return new() { Article = mapped };
	}
}