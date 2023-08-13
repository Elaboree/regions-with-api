using Cleverbit.CodingCase.Api.Attributes;
using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Requests;
using Cleverbit.CodingCase.Application.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Cleverbit.CodingCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : BaseController
{
    public EmployeeController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet()]
    [SwaggerOperation(Description = MethodDescription.GetEmployees)]
    [CustomHttpResponse(HttpStatusCode.BadRequest)]
    [CustomHttpResponse(HttpStatusCode.InternalServerError)]
    [CustomHttpResponse(HttpStatusCode.OK)]
    public async Task<IActionResult> Add()
    {
        var query = new BaseRequest();
        return Result(await Mediator.Send(query));
    }

    [HttpPost()]
    [SwaggerOperation(Description = MethodDescription.AddEmployee)]
    [CustomHttpResponse(HttpStatusCode.BadRequest)]
    [CustomHttpResponse(HttpStatusCode.InternalServerError)]
    [CustomHttpResponse(HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromBody] AddEmployeeCommand command)
    {
       return Result (await Mediator.Send(command));
    }
}