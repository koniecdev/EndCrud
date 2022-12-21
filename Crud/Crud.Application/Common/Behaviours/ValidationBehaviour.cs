using Microsoft.Extensions.Logging;

namespace Crud.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;
	private readonly ILogger<TRequest> _logger;
	public ValidationBehaviour(ILogger<TRequest> logger, IEnumerable<IValidator<TRequest>> validators)
	{
		_logger = logger;
		_validators = validators;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (_validators.Any())
		{
			var context = new ValidationContext<TRequest>(request);
			var failures = _validators.Select(m => m.Validate(context)).SelectMany(m => m.Errors).Where(m=>m != null).ToList();
			if(failures.Count != 0)
			{
				throw new ValidationException(failures);
			}
		}
		return await next();
	}
}
