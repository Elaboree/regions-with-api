using Cleverbit.CodingCase.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cleverbit.CodingCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;
    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IActionResult Result<T>(T response) where T : ServiceResponse
    {
        return response.StatusCode switch
        {
            (int)HttpStatusCode.BadRequest => BadRequest(response),
            (int)HttpStatusCode.InternalServerError => StatusCode(statusCode: response.StatusCode, response),
            _ => Ok(response),
        };
    }
}
