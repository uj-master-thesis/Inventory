
namespace Inventory.Domain.Model;

public class Thread
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime InsertDate { get; set; }
    public virtual IList<Post> Posts { get; set; }
}
