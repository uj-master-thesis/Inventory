using Inventory.Application.Response;
using MediatR;

namespace Inventory.Application.Queries.Post;

public class GetListOfPostsByEmailQuery : IRequest<List<GetPostByIdResponse>>
{
    public string Email { get; set; }
}