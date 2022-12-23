using Crud.Shared.Categories.Commands;

namespace Crud.Application.Categories.Commands;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
	public CreateCategoryCommandHandler(ICrudDbContext db)
	{

	}
	public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}