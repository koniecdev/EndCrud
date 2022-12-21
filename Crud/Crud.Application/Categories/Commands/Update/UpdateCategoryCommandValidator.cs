using Crud.Shared.Categories.Commands;

namespace Crud.Application.Countries.Commands;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
	public UpdateCategoryCommandValidator()
	{
		RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
		RuleFor(m => m.Name).MinimumLength(3).MaximumLength(100);
	}
}
