
namespace Inventory.Application.Interfaces;

public interface IWriteRepository<T> where T : class
{
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(Guid id);
}
