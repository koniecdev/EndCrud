using Crud.Shared.Members.Commands;

namespace Crud.Application.Members.Commands;
public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Unit>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public UpdateMemberCommandHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<Unit> Handle(UpdateMemberCommand command, CancellationToken cancellationToken)
	{
		Member? member = await _db.Members.SingleOrDefaultAsync(m => m.UserId == command.UserId, cancellationToken);
		if(member == null)
		{
			throw new NotFoundException(command.UserId);
		}
		_mapper.Map(command, member);
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
