namespace Inventory.Application.Interfaces.ReadRepositories;

public interface IReadRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> Get(string id);
}
