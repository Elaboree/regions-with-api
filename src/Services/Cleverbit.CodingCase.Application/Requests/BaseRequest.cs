using Cleverbit.CodingCase.Application.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Cleverbit.CodingCase.Application.Requests;
public class BaseRequest : IRequest<ServiceResponse>
{
    [JsonIgnore]
    public string? RequestId { get; set; }
}