using Crud.Shared.Categories.Queries;

namespace Crud.Application.Categories.Commands;
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetAllCategoriesQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetAllCategoriesVm> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Categories.Where(m=>m.StatusId != 0).ToListAsync(cancellationToken);
		GetAllCategoriesVm mapped = new() { Categories = _mapper.Map<List<GetAllCategoriesDto>>(fromDb) };
		if(mapped == null)
		{
			throw new MappingException(nameof(GetAllCategoriesVm), new Exception());
		}
		return mapped;
	}
}