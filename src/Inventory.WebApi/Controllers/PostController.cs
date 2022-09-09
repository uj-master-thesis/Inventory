using Inventory.Application.Queries.GestPostQuery;
using Inventory.Application.Queries.Post;
using Inventory.Application.Queries.Thread.GetListOfPostsQuery;
using Inventory.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Inventory.WebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediatior;
    public PostController(IMediator mediator)
    {
        _mediatior = mediator;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<GetPostResponse>), 200)]
    public async Task<IActionResult> GetList()
    {
        var posts = await _mediatior.Send(new GetListOfPostsQuery());
        return Ok(posts);
    }

    [HttpGet("{name}")]
    [ProducesResponseType(typeof(GetPostResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] string name)
    {
        var post = await _mediatior.Send(new GetPostByNameQuery() { Name = name });
        return post is not null ? Ok(post) : NotFound();
    }

    [HttpGet("by-user/{email}")]
    [ProducesResponseType(typeof(List<GetPostResponse>), 200)]
    public async Task<IActionResult> GeUserPosts([FromRoute] string email)
    {
        var post = await _mediatior.Send(new GetListOfPostsByEmailQuery() { Email = email });
        return Ok(post);
    }
}
