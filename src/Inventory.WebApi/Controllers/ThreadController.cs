using Inventory.Application.Queries;
using Inventory.Application.Queries.GetThreadByIdQuery;
using Inventory.Application.Queries.Thread.GetListOfThreadsQuery;
using Inventory.Application.Response;
using Inventory.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Inventory.WebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/thread")]
public class ThreadController : ControllerBase
{
    private readonly IMediator _mediatior;
    public ThreadController(IMediator mediator)
    {
        _mediatior = mediator;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<GetThreadByIdResponse>), 200)]
    public async Task<IActionResult> GetList()
    {
        var threads = await _mediatior.Send(new GetListOfThreadsQuery());
        return Ok(threads);
    }
}
