
using Inventory.Application.Interfaces;
using Inventory.Infractracture.DbConfiguration;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class WriteRepository<T> : IWriteRepository<T>  where T : class
{
    private readonly InventoryContext _inventoryContext;
    private readonly ILogger<WriteRepository<T>> _logger;
    public WriteRepository(ILogger<WriteRepository<T>>  logger , InventoryContext inventoryContext)
    {
        _logger = logger; 
        _inventoryContext = inventoryContext;
    }
    public async Task<T> Add(T entity)
    {
       _logger.LogInformation($"Added entity to db. Type: {typeof(T)}");
        await _inventoryContext.Set<T>().AddAsync(entity);                              
        await _inventoryContext.SaveChangesAsync();                             
        return entity; 
    }

    public async Task<T> Delete(Guid id)
    {
        _logger.LogInformation($"Delete entity to db. Type: {typeof(T)}");
        var entity = await _inventoryContext.Set<T>().FindAsync(id); 
         _inventoryContext.Set<T>().Remove(entity);
        await _inventoryContext.SaveChangesAsync();
        return entity;
        
    }

    public async Task<T> Update(T entity)
    {
        _logger.LogInformation($"Update entity to db. Type: {typeof(T)}");
        _inventoryContext.Set<T>().Update(entity);
        await _inventoryContext.SaveChangesAsync();
        return entity; 
    }
}
                                           