using Crud.Application.Categories.Commands;
using Crud.Shared.Categories.Commands;

namespace Crud.UnitTests.Categories.Commands;
public class UpdateCategoryCommandHandlerTest : CommandTestBase
{
	private readonly UpdateCategoryCommandHandler _handler;
	public UpdateCategoryCommandHandlerTest()
	{
		_handler = new(_db);
	}

	[Fact]
	public async Task UpdateCategory()
	{
		UpdateCategoryCommand command = new()
		{
			Id = 1,
			Name = "Edited category"
		};
		await _handler.Handle(command, _token);
		var fromDb = await _db.Categories.FirstOrDefaultAsync(m => m.Id == command.Id && m.StatusId == 1);
		fromDb.Name.ShouldBe(command.Name);
	}
}