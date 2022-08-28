using Inventory.Application.Queries;
using Inventory.Application.Queries.GetThreadByIdQuery;
using Inventory.Application.Queries.Thread.GetListOfThreadsQuery;
using Inventory.Application.Response;
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

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetThreadByIdResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var post = await _mediatior.Send(new GetThreadByIdQuery() { Id = id });
        return Ok(post);
    }

    [HttpGet("{id}/posts")]
    [ProducesResponseType(typeof(GetThreadByIdResponse), 200)]
    public async Task<IActionResult> GetThreadPosts([FromRoute] Guid id)
    {
        var post = await _mediatior.Send(new GetThreadWithPostsQuery() { ThreadId = id });
        return Ok(post);
    }
}
