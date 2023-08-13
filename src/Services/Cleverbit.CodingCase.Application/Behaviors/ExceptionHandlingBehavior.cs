using Cleverbit.CodingCase.Application.Models;
using Cleverbit.CodingCase.Application.Requests;
using Cleverbit.CodingCase.Application.Responses;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cleverbit.CodingCase.Application.Behaviors;
public class ExceptionHandlingBehavior<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse?, TException>
 where TRequest : BaseRequest, IRequest<TResponse>
 where TException : Exception
 where TResponse : ServiceResponse
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse, TException>> logger;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse, TException>> logger)
    {
        this.logger = logger;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse?> state, CancellationToken cancellationToken)
    {
        var error = CreateExceptionError(exception);

        logger.LogError(request.RequestId, request.GetType().Name, JsonSerializer.Serialize(error));

        var response = ServiceResponse.CreateExceptionError();

        state.SetHandled(response as TResponse);

        return Task.FromResult(response);
    }

    private static ExceptionModel CreateExceptionError(TException exception)
    {
        var methodName = exception.TargetSite?.DeclaringType?.DeclaringType?.FullName;
        var message = exception.Message;
        var innerException = exception.InnerException?.Message;
        var stackTrace = exception.StackTrace;

        return new ExceptionModel(methodName, message, innerException, stackTrace);
    }
}

