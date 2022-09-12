using Inventory.Domain.Model;

namespace Inventory.Application.Interfaces.WriteRepositories;

public interface IPostWriteRepository : IWriteRepository<Post>
{
    Task UpdateTimespan(string id);
    Task UpdatePostVotes(string id);
}
