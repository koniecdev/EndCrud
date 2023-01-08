using Crud.Application.Articles.Commands;
using Crud.Shared.Articles.Queries;

namespace Crud.UnitTests.Pictures.Queries;

[Collection("QueryCollection")]
public class GetAllArticlesQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetAllArticlesQueryHandler _handler;
	public GetAllArticlesQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetAllArticlesQueryTest()
	{
		var response = await _handler.Handle(new GetAllArticlesQuery(), _token);
		response.Articles.Count.ShouldBe(2);
	}
}