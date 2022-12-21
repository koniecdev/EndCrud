using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Crud.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly ILogger<TRequest> _logger;
	private readonly Stopwatch _stopwatch;
	public PerformanceBehaviour(ILogger<TRequest> logger)
	{
		_logger = logger;
		_stopwatch = new();
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		_stopwatch.Start();
		var response = await next();
		_stopwatch.Stop();
		var elapsed = _stopwatch.ElapsedMilliseconds;
		if(elapsed > 500)
		{
			var requestName = typeof(TRequest).Name;
			_logger.LogInformation("EndPlanner Long Running request: {Name} ({elapsed} ms) {@Request}", requestName, elapsed, request);
		}
		return response;
	}
}
