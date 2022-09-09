
using Inventory.Domain.Model;

namespace Inventory.Application.Interfaces.ReadRepositories;

public interface IReadCommentRepository : IReadRepository<Domain.Model.Comment>
{
    public Task<List<Comment>> GetListOfCommentsByUser(string user);
    public Task<List<Comment>> GetListOfCommentsByPostName(string postName);
}
