using Dapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Inventory.Infractracture.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class ThreadReadRepository : ReadBaseRepository<Domain.Model.Thread>, IReadThreadRepository
{
    private readonly InventoryReadContext _context;
    private readonly InventoryWriteContext _contextTwo;

    public ThreadReadRepository(ILogger<ReadBaseRepository<Domain.Model.Thread>> logger, InventoryReadContext context, InventoryWriteContext inventoryWrite) : base(logger, context)
    {
        _context = context; 
        _contextTwo = inventoryWrite;
    }

    protected override string QueryStringAll  => "SELECT * FROM Threads"; 
    protected override string QueryStringFirst  => $"{QueryStringAll} WHERE Id = @Id";
     
    public async Task<Domain.Model.Thread> GetThreadWithPosts(Guid id)
    {
        var query = $"{QueryStringAll} LEFT JOIN Posts ON Posts.ThreadId = Threads.Id WHERE Threads.Id = @Id";
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<Domain.Model.Thread, Post, Domain.Model.Thread>(query, (t, p) =>
        {
            if (p is null) return t;
            (t.Posts ??= new List<Post>()).Add(p); 
            return t;
        }, DynamincParametersFactory.CreateFromArray(new[] { ("@Id", $"{id}") }));
        return result.FirstOrDefault(); 
    }

    public async override Task<List<Domain.Model.Thread>> GetAll()
    {
        return await _contextTwo.Threads.ToListAsync();
    }
}
