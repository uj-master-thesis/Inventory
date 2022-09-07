
using Inventory.Application.Responses;

namespace Inventory.Application.Response;

public class GetPostResponse
{
    public string PostName { get; set; }
    public Uri? Uri { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public DateTime TimeStamp { get; set; }
    public List<CommentResponse> Comments { get; set; }
}
