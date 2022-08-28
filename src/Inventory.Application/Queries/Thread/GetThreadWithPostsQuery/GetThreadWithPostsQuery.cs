using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries;

public class GetThreadWithPostsQuery : IRequest<GetThreadByIdWithPostsResponse>
{
    public Guid ThreadId { get; set; }
}
