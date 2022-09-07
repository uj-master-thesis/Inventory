using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.Post;

public class GetListOfPostsByEmailQuery : IRequest<List<GetPostResponse>>
{
    public string Email { get; set; }
}