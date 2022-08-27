
using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.GetThreadByIdQuery;

public class GetThreadByIdQuery : IRequest<GetThreadByIdResponse>
{
    public Guid Id { get; set; }
}
