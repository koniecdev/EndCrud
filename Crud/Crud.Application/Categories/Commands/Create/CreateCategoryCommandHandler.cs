namespace Crud.Shared.Categories.Commands;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
	public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}