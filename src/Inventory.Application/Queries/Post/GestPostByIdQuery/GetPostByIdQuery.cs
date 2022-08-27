using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.GestPostQuery;

public class GetPostByIdQuery : IRequest<GetPostByIdResponse>
{
    public Guid Id { get; set; }
}
