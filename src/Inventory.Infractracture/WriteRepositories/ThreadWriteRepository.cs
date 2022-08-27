using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class ThreadWriteRepository : WriteBaseRepository<Domain.Model.Thread>, IThreadWriteRepository
{
    public ThreadWriteRepository(ILogger<WriteBaseRepository<Domain.Model.Thread>> logger, InventoryWriteContext inventoryContext) : base(logger, inventoryContext)
    {
    }
}
