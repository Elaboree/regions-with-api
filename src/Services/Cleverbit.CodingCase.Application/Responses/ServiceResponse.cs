using Cleverbit.CodingCase.Application.Constants;
using System.Net;

namespace Cleverbit.CodingCase.Application.Responses
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public object? Data { get; init; }

        public static ServiceResponse CreateSuccess(string? message)
        {
            return CreateServiceResponse(true, message, (int)HttpStatusCode.OK, null);
        }

        public static ServiceResponse CreateSuccess(string? message, object? data)
        {
            return CreateServiceResponse(true, message, (int)HttpStatusCode.OK, data);
        }

        public static ServiceResponse CreateExceptionError()
        {
            return CreateServiceResponse(false, ResponseMessage.SystemError, (int)HttpStatusCode.InternalServerError, null);
        }

        public static ServiceResponse CreateExceptionError(string? message)
        {
            return CreateServiceResponse(false, message, (int)HttpStatusCode.InternalServerError, null);
        }

        public static ServiceResponse CreateError(string? message)
        {
            message = CreateErrorMessage(message);

            return CreateServiceResponse(false, message, (int)HttpStatusCode.InternalServerError, null);
        }

        public static ServiceResponse CreateError(string? message, int statusCode)
        {
            message = CreateErrorMessage(message);

            return CreateServiceResponse(false, message, statusCode, null);
        }

        private static string CreateErrorMessage(string? message)
        {
            return string.IsNullOrEmpty(message)
                ? ResponseMessage.SystemError
                : message;
        }

        private static ServiceResponse CreateServiceResponse(bool success, string? message, int statusCode, object? data)
        {
            return new ServiceResponse
            {
                Success = success,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }
    }
}
