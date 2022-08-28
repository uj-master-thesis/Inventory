
using Inventory.Application.Response;

namespace Inventory.Application.Responses;

public class GetThreadByIdWithPostsResponse : GetThreadByIdResponse
{
    List<GetPostByIdResponse> Posts; 
}
