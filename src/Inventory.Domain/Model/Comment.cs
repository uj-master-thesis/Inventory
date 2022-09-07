
namespace Inventory.Domain.Model;

public class Comment
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string UserName { get; set; }
    public DateTime TimeStamp { get; set; }
    public string PostName { get; set; }
    public virtual Post Post { get; set; }
}