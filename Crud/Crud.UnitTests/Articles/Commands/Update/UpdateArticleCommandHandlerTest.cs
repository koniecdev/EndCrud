using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Commands;

namespace Crud.UnitTests.Articles.Commands;
public class UpdateArticleCommandHandlerTest : CommandTestBase
{
	private readonly UpdateArticleCommandHandler _handler;
	public UpdateArticleCommandHandlerTest()
	{
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task UpdateArticle()
	{
		UpdateArticleCommand command = new()
		{
			Id = 1,
			Header = "New edited header"
		};
		await _handler.Handle(command, _token);
		var fromDb = await _db.Articles.FirstOrDefaultAsync(m => m.Id == command.Id && m.StatusId == 1);
		fromDb?.Header.ShouldBe(command.Header);
	}
}