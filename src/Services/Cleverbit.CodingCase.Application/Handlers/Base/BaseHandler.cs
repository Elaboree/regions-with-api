using MediatR;
using Cleverbit.CodingCase.Application.Requests;
using Cleverbit.CodingCase.Application.Responses;
using Microsoft.Extensions.Logging;

namespace Cleverbit.CodingCase.Application.Handlers.Base
{
    public abstract class BaseHandler<TRequest, THandler> : IRequestHandler<TRequest, ServiceResponse> where TRequest : BaseRequest
    {
        protected readonly ILogger<THandler> Logger;

        protected BaseHandler(ILogger<THandler> logger)
        {
            Logger = logger;
        }

        public abstract Task<ServiceResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected void LogError(string? requestId, string message, params object[] args)
        {
            var eventId = new EventId(0, requestId);
            Logger.LogError(eventId, message, args);
        }
    }
}
