using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Commands;

namespace Crud.UnitTests.Pictures.Commands;
public class DeleteArticleCommandHandlerTest : CommandTestBase
{
	private readonly DeleteArticleCommandHandler _handler;
	public DeleteArticleCommandHandlerTest()
	{
		_handler = new(_db);
	}

	[Fact]
	public async Task DeleteArticle()
	{
		DeleteArticleCommand command = new()
		{
			Id = 1
		};
		await _handler.Handle(command, _token);
		var fromDb = await _db.Articles.FirstOrDefaultAsync(m => m.Id == command.Id && m.StatusId == 1);
		fromDb.ShouldBeNull();
	}
}