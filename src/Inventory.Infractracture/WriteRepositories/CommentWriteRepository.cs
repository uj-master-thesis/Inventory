
using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class CommentWriteRepository : WriteBaseRepository<Comment>, ICommentWriteRepository
{
    public CommentWriteRepository(ILogger<CommentWriteRepository> logger, InventoryWriteContext inventoryContext) : base(logger, inventoryContext)
    {
    }
}
