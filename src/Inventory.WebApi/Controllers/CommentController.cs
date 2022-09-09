using Inventory.Application.Queries.Comment.GetPostCommentQuery;
using Inventory.Application.Queries.Comment.GetUserCommentQuery;
using Inventory.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Inventory.WebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediatior;
    public CommentController(IMediator mediator)
    {
        _mediatior = mediator;
    }

    [HttpGet("by-post/{postName}")]
    [ProducesResponseType(typeof(List<CommentResponse>), 200)]
    public async Task<IActionResult> GetByPostName([FromRoute] string postName)
    {
        var comments = await _mediatior.Send(new GetPostCommentQuery() { PostName = postName});
        return Ok(comments);
    }

    [HttpGet("by-user/{userName}")]
    [ProducesResponseType(typeof(List<CommentResponse>), 200)]
    public async Task<IActionResult> GetByUserName([FromRoute] string userName)
    {
        var comments = await _mediatior.Send(new GetUserCommentQuery() { UserName = userName});
        return Ok(comments);
    }

}
