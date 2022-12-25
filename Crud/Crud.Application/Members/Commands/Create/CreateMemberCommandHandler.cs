using Crud.Shared.Members.Commands;

namespace Crud.Application.Members.Commands;
public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, int>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public CreateMemberCommandHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<int> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
	{
		Member? member = _mapper.Map<Member>(command);
		if(member == null)
		{
			throw new MappingException(nameof(CreateMemberCommand), new Exception());
		}
		_db.Members.Add(member);
		await _db.SaveChangesAsync(cancellationToken);
		if(member.Id == 0)
		{
			throw new DatabaseException(member.Id.ToString(), new Exception());
		}
		return member.Id;
	}
}
