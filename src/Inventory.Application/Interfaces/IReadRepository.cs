
namespace Inventory.Infractracture.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> Get(Guid id);
}
