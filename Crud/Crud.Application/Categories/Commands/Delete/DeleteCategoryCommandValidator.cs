using Crud.Shared.Categories.Commands;

namespace Crud.Application.Countries.Commands;
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
	public DeleteCategoryCommandValidator()
	{
		RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
	}
}
