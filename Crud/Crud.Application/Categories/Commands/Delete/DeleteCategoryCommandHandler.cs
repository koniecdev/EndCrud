using Crud.Shared.Categories.Commands;

namespace Crud.Application.Categories.Commands;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
	public DeleteCategoryCommandHandler(ICrudDbContext db)
	{

	}
	public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		return Unit.Value;
	}
}