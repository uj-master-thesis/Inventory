
using Inventory.Application.Responses;

namespace Inventory.Application.Response;

public class GetPostResponse
{
    public string PostName { get; set; }
    public string ThreadName { get; set; }
    public Uri? Url { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public DateTime TimeStamp { get; set; }
    public int VoteCount { get; set; }
    public string FileCompressed { get; set; }

    public List<CommentResponse> Comments { get; set; }
}
