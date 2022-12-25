using Crud.Shared.Members.Queries;

namespace Crud.Application.Members.Queries;

public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, GetAllMembersVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public GetAllMembersQueryHandler(ICrudDbContext db, IMapper mapper)
	{
		_mapper = mapper;
		_db = db;
	}
	public async Task<GetAllMembersVm> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
	{
		var fromDb = await _db.Members.Where(m => m.StatusId != 0).ToListAsync(cancellationToken);
		var mapped = _mapper.Map<List<GetAllMembersDto>>(fromDb);
		if(mapped == null)
		{
			throw new MappingException(nameof(GetAllMembersDto), new Exception());
		}
		return new GetAllMembersVm() { Members = mapped };
	}
}
