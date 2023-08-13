using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


namespace Cleverbit.CodingCase.Api.Attributes
{
    public class CustomHttpResponseAttribute : SwaggerResponseAttribute
    {
        public CustomHttpResponseAttribute(
       HttpStatusCode responseStatusCode = 0,
       int statusCode = 0,
       string? description = null,
       Type? type = null) : base(statusCode, description, type)
        {
            StatusCode = (int)responseStatusCode;

            switch (responseStatusCode)
            {
                case HttpStatusCode.OK:
                    Description = ResponseMessage.OK;
                    break;
                case HttpStatusCode.BadRequest:
                    Description = ResponseMessage.BadRequest;
                    Type = typeof(ValidationErrorResponse);
                    break;
                case HttpStatusCode.InternalServerError:
                    Description = ResponseMessage.SystemError;
                    Type = typeof(ServiceResponse);
                    break;
                default:
                    Type = typeof(ServiceResponse);
                    break;
            }
        }
    }
}
