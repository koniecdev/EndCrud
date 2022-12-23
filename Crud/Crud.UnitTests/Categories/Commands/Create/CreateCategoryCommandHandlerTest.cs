using Crud.Application.Categories.Commands;
using Crud.Shared.Categories.Commands;

namespace Crud.UnitTests.Categories.Commands;
public class CreateCategoryCommandHandlerTest : CommandTestBase
{
	private readonly CreateCategoryCommandHandler _handler;
	public CreateCategoryCommandHandlerTest()
	{
		_handler = new(_db);
	}

	[Fact]
	public async Task CreateCategory()
	{
		CreateCategoryCommand command = new()
		{
			Name = "Information"
		};
		var returnedId = await _handler.Handle(command, _token);
		var fromDb = await _db.Categories.FirstOrDefaultAsync(m => m.Id == returnedId && m.StatusId == 1);
		fromDb.ShouldNotBeNull();
	}
}