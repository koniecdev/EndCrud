using Crud.Application.Categories.Commands;
using Crud.Application.Members.Commands;
using Crud.Shared.Categories.Commands;
using Crud.Shared.Members.Commands;

namespace Crud.UnitTests.Categories.Commands;
public class UpdateMemberCommandHandlerTest : CommandTestBase
{
	private readonly UpdateMemberCommandHandler _handler;
	public UpdateMemberCommandHandlerTest()
	{
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task UpdateMember()
	{
		UpdateMemberCommand command = new()
		{
			UserId = "UserIdOfMember",
			Username = "Edited Username"
		};
		await _handler.Handle(command, _token);
		var fromDb = await _db.Members.FirstOrDefaultAsync(m => m.UserId.Equals(command.UserId) && m.StatusId == 1);
		fromDb.Username.ShouldBe(command.Username);
	}
}