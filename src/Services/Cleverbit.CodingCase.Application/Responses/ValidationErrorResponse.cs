using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Models;
using System.Net;

namespace Cleverbit.CodingCase.Application.Responses
{
    public class ValidationErrorResponse : ServiceResponse
    {
        public List<ValidationError> Errors { get; set; }

        public ValidationErrorResponse(List<ValidationError> errors)
        {
            Success = false;
            Message = ResponseMessage.BadRequest;
            StatusCode = (int)HttpStatusCode.BadRequest;
            Errors = errors;
        }

        public static ValidationErrorResponse Create(List<ValidationError> errors)
        {
            return new ValidationErrorResponse(errors);
        }
    }
}
