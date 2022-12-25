using Crud.Application.Members.Commands;
using Crud.Shared.Members.Commands;

namespace Crud.UnitTests.Members.Commands;
public class CreateMemberCommandHandlerTest : CommandTestBase
{
	private readonly CreateMemberCommandHandler _handler;
	public CreateMemberCommandHandlerTest()
	{
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task CreateMember()
	{
		CreateMemberCommand command = new()
		{
			UserId = "somefakeid",
			Email = "Lmfao@gmail.com",
			Username = "usernejm"
		};
		var returnedId = await _handler.Handle(command, _token);
		var fromDb = await _db.Members.FirstOrDefaultAsync(m => m.Id == returnedId && m.StatusId == 1);
		fromDb.ShouldNotBeNull();
	}
}