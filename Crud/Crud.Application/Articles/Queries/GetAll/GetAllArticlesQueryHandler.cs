using Crud.Shared.Articles.Queries;

namespace Crud.Application.Articles.Commands;
public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, GetAllArticlesVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetAllArticlesQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetAllArticlesVm> Handle(GetAllArticlesQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Articles.Where(m => m.StatusId != 0).ToListAsync(cancellationToken);
		GetAllArticlesVm mapped = new() { Articles = _mapper.Map<List<GetAllArticlesDto>>(fromDb) };
		if (mapped == null)
		{
			throw new MappingException(nameof(GetAllArticlesVm), new Exception());
		}
		return mapped;
	}
}