using Cleverbit.CodingCase.Api.Attributes;
using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Cleverbit.CodingCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController : BaseController
{
    public RegionController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost()]
    [SwaggerOperation(Description = MethodDescription.AddRegion)]
    [CustomHttpResponse(HttpStatusCode.BadRequest)]
    [CustomHttpResponse(HttpStatusCode.InternalServerError)]
    [CustomHttpResponse(HttpStatusCode.OK)]
    [CustomHttpResponse(HttpStatusCode.NotFound)]
    public async Task<IActionResult> Add([FromBody] AddRegionCommand command)
    {
        return Result(await Mediator.Send(command));
    }
}
