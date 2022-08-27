using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.Thread.GetListOfPostsQuery;

public class GetListOfPostsQuery : IRequest<List<GetPostByIdResponse>>
{
}