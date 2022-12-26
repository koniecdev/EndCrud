using Crud.Shared.Articles.Commands;

namespace Crud.Application.Articles.Commands;
public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
	public UpdateArticleCommandValidator()
	{
		RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
		RuleFor(m => m.Header).MinimumLength(3).MaximumLength(100);
		RuleFor(m => m.Content).MinimumLength(3).MaximumLength(100);
		RuleFor(m => m.MemberId).GreaterThan(0).LessThan(int.MaxValue);
		RuleFor(m => m.CategoryId).GreaterThan(0).LessThan(int.MaxValue);
	}
}
