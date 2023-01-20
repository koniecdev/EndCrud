using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Queries;

namespace Crud.UnitTests.Articles.Queries;

[Collection("QueryCollection")]
public class GetCategoriesQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetCategoriesQueryHandler _handler;
	public GetCategoriesQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetAllArticlesQueryTest()
	{
		var response = await _handler.Handle(new GetCategoriesQuery(), _token);
		response.Categories.Count.ShouldBe(2);
	}
}