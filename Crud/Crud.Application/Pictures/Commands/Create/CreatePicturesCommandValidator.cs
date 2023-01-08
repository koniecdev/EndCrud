using Crud.Shared.Articles.Commands;

namespace Crud.Application.Pictures.Commands;
public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
	public CreateArticleCommandValidator()
	{
		RuleFor(m => m.Header).MinimumLength(3).MaximumLength(100).NotEmpty();
		RuleFor(m => m.Content).MinimumLength(3).MaximumLength(100).NotEmpty();
		RuleFor(m => m.MemberId).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
		RuleFor(m => m.CategoryId).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
	}
}
