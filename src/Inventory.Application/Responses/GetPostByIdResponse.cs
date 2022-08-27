
namespace Inventory.Application.Response;

public class GetPostByIdResponse
{
    public Guid Id { get; set; }
    public string PostName { get; set; }
    public Uri? Uri { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
}
