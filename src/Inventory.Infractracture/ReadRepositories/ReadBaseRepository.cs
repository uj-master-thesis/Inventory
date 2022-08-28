using Dapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.Utils;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal abstract class ReadBaseRepository<T> : IReadRepository<T> where T : class
{
    private readonly ILogger<ReadBaseRepository<T>> _logger; 
    private readonly InventoryReadContext _context;
    protected abstract string QueryStringAll { get;  }
    protected abstract string QueryStringFirst { get; }
    public ReadBaseRepository(ILogger<ReadBaseRepository<T>> logger, InventoryReadContext context)
    {
        _logger = logger;
        _context = context;
    }

    protected async Task<List<T>> QueryList(string query, DynamicParameters param = null)
    {
        _logger.LogInformation($"Get list of entitities. Type: {typeof(T)}");
        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<T>(query, param);
        return entities.ToList();
    }

    protected async Task<T> QueryFirst(string query, DynamicParameters param)
    {
        _logger.LogInformation($"Get single entity. Type: {typeof(T)}");
        using var connection = _context.CreateConnection();
        var entity = await connection.QueryFirstOrDefaultAsync<T>(query, param);
        return entity;
    }

    public async Task<List<T>> GetAll()
    {
        return await QueryList(QueryStringAll);
    }

    public async Task<T> Get(Guid id)
    {
        return await QueryFirst(QueryStringFirst, DynamincParametersFactory.CreateFromArray(new[] { ("@Id", $"{id}") }));
    }
}
