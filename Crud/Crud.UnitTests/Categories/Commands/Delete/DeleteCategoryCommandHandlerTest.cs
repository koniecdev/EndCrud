using Crud.Application.Categories.Commands;
using Crud.Shared.Categories.Commands;

namespace Crud.UnitTests.Categories.Commands;
public class DeleteCategoryCommandHandlerTest : CommandTestBase
{
	private readonly DeleteCategoryCommandHandler _handler;
	public DeleteCategoryCommandHandlerTest()
	{
		_handler = new(_db);
	}

	[Fact]
	public async Task DeleteCategory()
	{
		DeleteCategoryCommand command = new(1);
		await _handler.Handle(command, _token);
		var fromDb = await _db.Categories.FirstOrDefaultAsync(m => m.Id == command.Id && m.StatusId == 1);
		fromDb.ShouldBeNull();
	}
}