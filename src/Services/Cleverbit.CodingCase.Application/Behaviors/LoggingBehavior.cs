using Cleverbit.CodingCase.Application.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cleverbit.CodingCase.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : BaseRequest, IRequest<TResponse>
{
    private readonly ILogger<TRequest> logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestWithGuid = $"{request.GetType().Name} [{request.RequestId}]";

        using var scope = logger.BeginScope(requestWithGuid);

        LogInformation(request, "{START}", DateTime.Now.ToLongDateString());

        try
        {
            LogRequestWithProps(request, requestWithGuid);

            return await next.Invoke();
        }
        finally
        {
            LogInformation(request, "{END}", DateTime.Now.ToLongDateString());
            LogInformation(request, "{Method}", requestWithGuid);
        }
    }

    private void LogRequestWithProps(TRequest request, string requestWithGuid)
    {
        try
        {
            LogInformation(request, "{Method}", requestWithGuid);
            LogInformation(request, "{Parameters}", JsonSerializer.Serialize(request));
        }
        catch (NotSupportedException)
        {
            LogInformation(request, "{Serialization ERROR}", $"{requestWithGuid} Could not serialize the request.");
        }
    }

    protected void LogInformation(TRequest request, string message, params object[] args)
    {
        var eventId = new EventId(0, request.RequestId);

        logger.LogInformation(eventId: eventId, message: message, args: args);
    }
}

