using Crud.Shared.Pictures.Queries;

namespace Crud.Application.Pictures.Commands;
public class GetAllPicturesQueryHandler : IRequestHandler<GetAllPicturesQuery, GetAllPicturesVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetAllPicturesQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<GetAllPicturesVm> Handle(GetAllPicturesQuery query, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Pictures.Where(m => m.StatusId != 0).ToListAsync(cancellationToken);
		GetAllPicturesVm mapped = new() { Pictures = _mapper.Map<List<GetAllPicturesDto>>(fromDb) };
		if (mapped == null)
		{
			throw new MappingException(nameof(GetAllPicturesVm), new Exception());
		}
		return mapped;
	}
}