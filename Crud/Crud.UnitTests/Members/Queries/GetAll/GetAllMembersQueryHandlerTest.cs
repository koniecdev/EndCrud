using Crud.Application.Categories.Commands;
using Crud.Application.Members.Queries;
using Crud.Shared.Categories.Queries;
using Crud.Shared.Members.Queries;

namespace Crud.UnitTests.Categories.Queries;

[Collection("QueryCollection")]
public class GetAllMembersQueryHandlerTest : QueryTestFixtures
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly GetAllMembersQueryHandler _handler;
	public GetAllMembersQueryHandlerTest(QueryTestFixtures fixtures)
	{
		_db = fixtures.Context;
		_mapper = fixtures.Mapper;
		_handler = new(_db, _mapper);
	}

	[Fact]
	public async Task GetAllMembersQueryTest()
	{
		var response = await _handler.Handle(new GetAllMembersQuery(), _token);
		response.Members.Count.ShouldBe(2);
	}
}