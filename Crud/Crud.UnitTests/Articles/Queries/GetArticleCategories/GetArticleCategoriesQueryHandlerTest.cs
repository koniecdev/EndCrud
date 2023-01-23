using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Queries;

namespace Crud.UnitTests.Articles.Queries;

[Collection("QueryCollection")]
public class GetArticleCategoriesQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetArticleCategoriesQueryHandler _handler;
	public GetArticleCategoriesQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetArticleCategoriesQueryTest()
	{
		var response = await _handler.Handle(new GetArticleCategoriesQuery(1), _token);
		response.Article.GalleryString.ShouldBe("1,2");
	}
}