using Inventory.Application.Queries.GetThreadByIdQuery;
using Inventory.Application.Queries.Thread.GetListOfThreadsQuery;
using Inventory.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers;

[Route("api/thread")]
public class ThreadController : ControllerBase
{
    private readonly IMediator _mediatior;
    public ThreadController(IMediator mediator)
    {
        _mediatior = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetThreadByIdResponse), 200)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var post = await _mediatior.Send(new GetThreadByIdQuery() { Id = id });
        return Ok(post);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<GetThreadByIdResponse>), 200)]
    public async Task<IActionResult> GetList()
    {
        var threads = await _mediatior.Send(new GetListOfThreadsQuery());
        return Ok(threads);
    }
}
