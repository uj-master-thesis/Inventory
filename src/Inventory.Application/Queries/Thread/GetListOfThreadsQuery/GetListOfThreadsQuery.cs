using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.Thread.GetListOfThreadsQuery;

public class GetListOfThreadsQuery : IRequest<List<GetThreadByIdResponse>>
{
}
