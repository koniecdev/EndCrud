using Application.Members.Commands;
using Crud.Application.Categories.Commands;
using Crud.Shared.Categories.Commands;
using Crud.Shared.Members.Commands;

namespace Crud.UnitTests.Categories.Commands;
public class DeleteMemberCommandHandlerTest : CommandTestBase
{
	private readonly DeleteMemberCommandHandler _handler;
	public DeleteMemberCommandHandlerTest()
	{
		_handler = new(_db);
	}

	[Fact]
	public async Task DeleteMember()
	{
		DeleteMemberCommand command = new()
		{
			UserId = "UserIdOfMember"
		};
		await _handler.Handle(command, _token);
		var fromDb = await _db.Members.FirstOrDefaultAsync(m => m.Id.Equals(command.UserId) && m.StatusId == 1);
		fromDb.ShouldBeNull();
	}
}