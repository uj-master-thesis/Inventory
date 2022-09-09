using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries.Comment.GetUserCommentQuery;

public class GetUserCommentQuery : IRequest<List<CommentResponse>>
{
    public string UserName { get; set; }
}
