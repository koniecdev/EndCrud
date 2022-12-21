namespace Crud.Shared.Categories.Commands;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
	public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		return Unit.Value;
	}
}