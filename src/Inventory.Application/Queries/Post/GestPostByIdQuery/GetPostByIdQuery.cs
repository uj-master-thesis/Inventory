using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.GestPostQuery;

public class GetPostByIdQuery : IRequest<GetPostResponse>
{
    public Guid Id { get; set; }
}
