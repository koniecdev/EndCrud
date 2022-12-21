namespace Crud.Shared.Categories.Commands;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
	public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		return Unit.Value;
	}
}