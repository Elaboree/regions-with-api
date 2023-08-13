using Cleverbit.CodingCase.Api.Attributes;
using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Cleverbit.CodingCase.Api.Controllers;

[Route("api/region")]
[ApiController]
public class RegionEmployeeController : BaseController
{
    public RegionEmployeeController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{Id}/employees")]
    [SwaggerOperation(Description = MethodDescription.GetRegionEmployees)]
    [CustomHttpResponse(HttpStatusCode.BadRequest)]
    [CustomHttpResponse(HttpStatusCode.InternalServerError)]
    [CustomHttpResponse(HttpStatusCode.OK)]
    [CustomHttpResponse(HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get([FromRoute] int Id)
    {
        return Result(await Mediator.Send(new GetRegionEmployeeQuery { RegionId = Id }));
    }
}