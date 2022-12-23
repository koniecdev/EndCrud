using Crud.Shared.Categories.Commands;

namespace Crud.Application.Categories.Commands;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
	public UpdateCategoryCommandHandler(ICrudDbContext db)
	{

	}
	public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		return Unit.Value;
	}
}