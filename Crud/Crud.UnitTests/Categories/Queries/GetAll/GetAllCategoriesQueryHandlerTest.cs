using Crud.Application.Categories.Commands;
using Crud.Shared.Categories.Queries;

namespace Crud.UnitTests.Categories.Queries;

[Collection("QueryCollection")]
public class GetAllCategoriesQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetAllCategoriesQueryHandler _handler;
	public GetAllCategoriesQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetAllCategoriesQueryTest()
	{
		var response = await _handler.Handle(new GetAllCategoriesQuery(), _token);
		response.Categories.Count.ShouldBe(2);
	}
}