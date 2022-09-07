
using Inventory.Application.Response;

namespace Inventory.Application.Responses;

public class GetThreadByIdWithPostsResponse : GetThreadByIdResponse
{
    public List<GetPostResponse> Posts { get; set; }
    public int PostCount { get; set; }
}
