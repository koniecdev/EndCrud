using Crud.Shared.Categories.Queries;

namespace Crud.Shared.Categories.Commands;
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesVm>
{
	public async Task<GetAllCategoriesVm> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}