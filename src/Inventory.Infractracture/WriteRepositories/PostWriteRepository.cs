using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class PostWriteRepository : WriteBaseRepository<Post>, IPostWriteRepository
{
    public PostWriteRepository(ILogger<WriteBaseRepository<Post>> logger, InventoryWriteContext inventoryContext) : base(logger, inventoryContext)
    {
    }
}
