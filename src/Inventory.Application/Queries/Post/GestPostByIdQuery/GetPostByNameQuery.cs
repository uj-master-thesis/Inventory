using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.GestPostQuery;

public class GetPostByNameQuery : IRequest<GetPostResponse>
{
    public string Name { get; set; }
}
