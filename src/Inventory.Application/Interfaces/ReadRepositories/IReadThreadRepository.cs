
namespace Inventory.Application.Interfaces.ReadRepositories;

public interface IReadThreadRepository : IReadRepository<Domain.Model.Thread>
{
    Task<Domain.Model.Thread> GetThreadWithPosts(Guid Id); 
}
