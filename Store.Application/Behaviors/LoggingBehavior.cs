namespace Store.Application.Behaviors;

using MediatR;
using Microsoft.Extensions.Logging;

public sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>>
        _logger;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation(
            "Handling Request: {RequestName}",
            typeof(TRequest).Name);

        var response = await next();

        _logger.LogInformation(
            "Request Completed: {RequestName}",
            typeof(TRequest).Name);

        return response;
    }
}