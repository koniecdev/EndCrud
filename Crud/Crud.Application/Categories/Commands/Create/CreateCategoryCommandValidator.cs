using Crud.Shared.Categories.Commands;

namespace Crud.Application.Countries.Commands;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
	public CreateCategoryCommandValidator()
	{
		RuleFor(m => m.Name).MinimumLength(3).MaximumLength(100).NotEmpty();
	}
}
