
using Inventory.Application.Interfaces;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class PostWriteRepository : WriteRepository<Post>, IPostWriteRepository
{
    public PostWriteRepository(ILogger<WriteRepository<Post>> logger, InventoryContext inventoryContext) : base(logger, inventoryContext)
    {
    }
}
