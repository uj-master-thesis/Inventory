
namespace Inventory.Domain.Model;

public class Thread
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PostCount { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}
