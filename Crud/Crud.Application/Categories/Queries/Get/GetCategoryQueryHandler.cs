using Crud.Shared.Categories.Queries;

namespace Crud.Application.Categories.Commands;
public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetCategoryQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetCategoryVm> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Categories.SingleOrDefaultAsync(m=>m.StatusId != 0 && m.Id == query.Id, cancellationToken);
		GetCategoryVm mapped = new() { Category = _mapper.Map<GetCategoryDto>(fromDb) };
		if(mapped == null)
		{
			throw new MappingException(nameof(GetCategoryVm), new Exception());
		}
		return mapped;
	}
}