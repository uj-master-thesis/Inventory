using Inventory.Domain.Model;

namespace Inventory.Application.Interfaces.ReadRepositories;

public interface IReadPostRepository : IReadRepository<Post>
{
    Task<List<Post>> GetPostsByUser(string email);
}
