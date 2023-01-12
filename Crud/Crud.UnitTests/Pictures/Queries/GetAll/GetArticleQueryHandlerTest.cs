using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Queries;

namespace Crud.UnitTests.Pictures.Queries;

[Collection("QueryCollection")]
public class GetArticleQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetArticleQueryHandler _handler;
	public GetArticleQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetArticleQueryTest()
	{
		var response = await _handler.Handle(new(1), _token);
		var fromDb = await _db.Articles.SingleOrDefaultAsync(m => m.Id == 1, _token);
		(response.Article.Content.Equals(fromDb?.Content) && response.Article.MemberId == fromDb.MemberId).ShouldBeTrue();
	}
}