
namespace Inventory.Domain.Model;

public class Post
{
    public Guid Id { get; set; }
    public string PostName { get; set; }
    public Uri? Uri { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public Guid ThreadId { get; set; }
    public DateTime InsertDate { get; set; }
    public virtual Thread Thread { get; set; }
}
