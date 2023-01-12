using Crud.Application.Categories.Commands;
using Crud.Shared.Categories.Queries;

namespace Crud.UnitTests.Categories.Queries;

[Collection("QueryCollection")]
public class GetCategoryQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetCategoryQueryHandler _handler;
	public GetCategoryQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetCategoryQueryTest()
	{
		var response = await _handler.Handle(new GetCategoryQuery(1), _token);
		response.ShouldNotBeNull();
	}
}