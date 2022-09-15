using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal abstract class WriteBaseRepository<T> : IWriteRepository<T>  where T : class
{
    private readonly InventoryWriteContext _inventoryContext;
    private readonly ILogger<WriteBaseRepository<T>> _logger;
    public WriteBaseRepository(ILogger<WriteBaseRepository<T>>  logger , InventoryWriteContext inventoryContext)
    {
        _logger = logger; 
        _inventoryContext = inventoryContext;
    }
    virtual public async Task<T> Add(T entity)
    {
        try
        {
            _inventoryContext.ChangeTracker.Clear();
            _logger.LogInformation($"Added entity to db. Type: {typeof(T)}");
            await _inventoryContext.Set<T>().AddAsync(entity);
            await _inventoryContext.SaveChangesAsync();
            return entity;
        }
        catch (SqlException ex) when (ex.Number == -1 || ex.Number == 2 || ex.Number == 53)
        {
            throw new DBAccesException();
        }
    }

    virtual public async Task<T> Delete(Guid id)
    {
        try
        {
            _inventoryContext.ChangeTracker.Clear();
            _logger.LogInformation($"Delete entity to db. Type: {typeof(T)}");
            var entity = await _inventoryContext.Set<T>().FindAsync(id);
            _inventoryContext.Set<T>().Remove(entity);
            await _inventoryContext.SaveChangesAsync();
            return entity;
        }
        catch (SqlException ex) when (ex.Number == -1 || ex.Number == 2 || ex.Number == 53)
        {
            throw new DBAccesException();
        }
    }

    virtual public async Task<T> Update(T entity)
    {
        try
        {
            _inventoryContext.ChangeTracker.Clear();
            _logger.LogInformation($"Update entity to db. Type: {typeof(T)}");
            _inventoryContext.Set<T>().Update(entity);
            await _inventoryContext.SaveChangesAsync();
            return entity;
        }
        catch (SqlException ex) when (ex.Number == -1 || ex.Number == 2 || ex.Number == 53)
        {
            throw new DBAccesException(); 
        }
    }
}
                                           