
namespace Inventory.Domain.Model;

public class Thread
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime TimeStamp { get; set; }
    public virtual IList<Post> Posts { get; set; }
}