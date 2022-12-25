using Crud.Shared.Members.Commands;

namespace Crud.Application.Members.Commands;
public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
	public UpdateMemberCommandValidator()
	{
		RuleFor(m => m.Email).MaximumLength(200).MinimumLength(5).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
		RuleFor(m => m.Username).MaximumLength(200).MinimumLength(2);
	}
}
