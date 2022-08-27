

using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class ThreadReadRepository : ReadBaseRepository<Domain.Model.Thread>, IReadThreadRepository
{
    public ThreadReadRepository(ILogger<ReadBaseRepository<Domain.Model.Thread>> logger, InventoryReadContext context) : base(logger, context)
    {
    }

    protected override string QueryStringAll  => "SELECT * SELECT dbo.Threads"; 
    protected override string QueryStringFirst  => "SELECT * FROM  dbo.Threads WHERE Id = @Id"; 
}
