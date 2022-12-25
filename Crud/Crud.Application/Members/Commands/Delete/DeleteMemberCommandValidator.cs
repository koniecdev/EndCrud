using Crud.Shared.Members.Commands;

namespace Crud.Application.Members.Commands;
public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
{
	public DeleteMemberCommandValidator()
	{
		RuleFor(m => m.UserId).NotEmpty();
	}
}
