using Crud.Shared.Articles.Commands;

namespace Crud.Application.Articles.Commands;
public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
	public DeleteArticleCommandValidator()
	{
		RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
	}
}
