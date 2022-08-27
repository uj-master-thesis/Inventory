using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Application.Queries.GestPostQuery;
using Inventory.Application.Queries.Thread.GetListOfPostsQuery;
using Inventory.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers;

[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediatior;
    public PostController(IMediator mediator, IPostWriteRepository postWriteRepoistory)
    {
        _mediatior = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetPostByIdResponse), 200)]  
    public async Task<IActionResult> GetById([FromRoute] Guid id) 
    {
        var post = await _mediatior.Send(new GetPostByIdQuery() { Id = id });  
        return Ok(post);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<GetPostByIdResponse>), 200)]
    public async Task<IActionResult> GetList()
    {
        var posts = await _mediatior.Send(new GetListOfPostsQuery());
        return Ok(posts);
    }
}
