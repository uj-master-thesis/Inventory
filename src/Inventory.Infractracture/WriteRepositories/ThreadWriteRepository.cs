using Inventory.Application.Interfaces;
using Inventory.Infractracture.DbConfiguration;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class ThreadWriteRepository : WriteRepository<Domain.Model.Thread>, IThreadWriteRepository
{
    public ThreadWriteRepository(ILogger<WriteRepository<Domain.Model.Thread>> logger, InventoryContext inventoryContext) : base(logger, inventoryContext)
    {
    }
}
