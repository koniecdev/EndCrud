using Crud.Shared.Articles.Queries;

namespace Crud.Application.Articles.Commands;
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetCategoriesQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetCategoriesVm> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Categories
			.Where(m => m.StatusId != 0).ToListAsync(cancellationToken);
		GetCategoriesVm mapped = new() {
			Categories = _mapper.Map<List<GetCategoriesCategoryDto>>(fromDb)
		};
		if (mapped == null)
		{
			throw new MappingException(nameof(GetCategoriesVm), new Exception());
		}
		return mapped;
	}
}