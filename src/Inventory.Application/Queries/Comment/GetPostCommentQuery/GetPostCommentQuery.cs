using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries.Comment.GetPostCommentQuery;

public class GetPostCommentQuery : IRequest<List<CommentResponse>>
{
    public string PostName { get; set; }
}
