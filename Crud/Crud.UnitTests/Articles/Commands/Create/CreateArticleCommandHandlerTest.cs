using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Commands;

namespace Crud.UnitTests.Articles.Commands;
public class CreateArticleCommandHandlerTest : CommandTestBase
{
	private readonly CreateArticleCommandHandler _handler;
	public CreateArticleCommandHandlerTest()
	{
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task CreateArticle()
	{
		CreateArticleCommand command = new()
		{
			Header = "Lmaoooooooooo broooooooo",
			Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
			CategoryId = 1,
			MemberId = 1
		};
		var returnedId = await _handler.Handle(command, _token);
		var fromDb = await _db.Articles.FirstOrDefaultAsync(m => m.Id == returnedId && m.StatusId == 1);
		fromDb.ShouldNotBeNull();
	}
}