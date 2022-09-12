
namespace Inventory.Domain.Model;

public class Post
{
    public string Name { get; set; }
    public Uri? Url { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public DateTime TimeStamp { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public string ThreadName{ get; set; }
    public string FileCompressed { get; set; }
    public virtual Thread Thread { get; set; }
    public virtual IList<Comment> Comments { get; set; }
    public virtual IList<Vote> Votes { get; set; }
}