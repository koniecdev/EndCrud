using Crud.Shared.Articles.Queries;

namespace Crud.Application.Articles.Commands;
public class GetArticleCategoriesQueryHandler : IRequestHandler<GetArticleCategoriesQuery, GetArticleCategoriesVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetArticleCategoriesQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetArticleCategoriesVm> Handle(GetArticleCategoriesQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Categories
			.Where(m => m.StatusId != 0).ToListAsync(cancellationToken);
		var articleFromDb = await _db.Articles.Include(m => m.Pictures).SingleOrDefaultAsync(m=>m.Id == query.Id, cancellationToken);
		if(articleFromDb == null)
		{
			throw new NotFoundException(query.Id.ToString());
		}
		var mapped = _mapper.Map<GetArticleCategoriesArticleDto>(articleFromDb);
		if(mapped == null)
		{
			throw new MappingException(nameof(GetArticleCategoriesArticleDto), new Exception());
		}
		var idList = articleFromDb.Pictures?.OrderBy(m=>m.Id).Select(m => m.Id.ToString()).ToList();
		mapped.GalleryString = (idList == null) ? string.Empty : string.Join(',', idList);
		GetArticleCategoriesVm vm = new()
		{
			Article = mapped,
			Categories = _mapper.Map<List<GetArticleCategoriesCategoryDto>>(fromDb)
		};
		return vm;
	}
}