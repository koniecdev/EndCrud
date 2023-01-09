using Crud.Shared.Pictures.Commands;

namespace Crud.Application.Pictures.Commands;
public class DeletePictureCommandValidator : AbstractValidator<DeletePictureCommand>
{
	public DeletePictureCommandValidator()
	{
		RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
	}
}
