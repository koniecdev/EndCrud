using Crud.Shared.Pictures.Commands;

namespace Crud.Application.Pictures.Commands;
public class CreatePicturesCommandValidator : AbstractValidator<CreatePicturesCommand>
{
	public CreatePicturesCommandValidator()
	{
		RuleFor(m => m.Files).NotEmpty();
	}
}
