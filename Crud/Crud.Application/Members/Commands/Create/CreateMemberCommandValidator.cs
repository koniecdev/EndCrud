using Crud.Shared.Members.Commands;

namespace Crud.Application.Members.Commands;
public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
	public CreateMemberCommandValidator()
	{
		RuleFor(m => m.Email).MaximumLength(200).MinimumLength(5).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).NotEmpty();
		RuleFor(m => m.Username).MaximumLength(200).MinimumLength(2).NotEmpty();
		RuleFor(m => m.UserId).NotEmpty();
	}
}
