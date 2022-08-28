using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.Utils;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class ThreadReadRepository : ReadBaseRepository<Domain.Model.Thread>, IReadThreadRepository
{
    public ThreadReadRepository(ILogger<ReadBaseRepository<Domain.Model.Thread>> logger, InventoryReadContext context) : base(logger, context)
    {
    }

    protected override string QueryStringAll  => "SELECT * FROM Threads"; 
    protected override string QueryStringFirst  => $"{QueryStringAll} WHERE Id = @Id";
     
    public async Task<Domain.Model.Thread> GetThreadWithPosts(Guid id)
    {
        var query = $"{QueryStringAll} INNER JOIN Posts ON Posts.ThreadId = Threads.Id WHERE Threads.Id = @Id"; 
        return await QueryFirst(query, DynamincParametersFactory.CreateFromArray(new[] { ("@Id", $"{id}") })); 
    }
}
