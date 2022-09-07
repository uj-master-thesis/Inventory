using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class PostWriteRepository : WriteBaseRepository<Post>, IPostWriteRepository
{
    private readonly InventoryWriteContext _context; 
    public PostWriteRepository(ILogger<WriteBaseRepository<Post>> logger, InventoryWriteContext inventoryContext) : base(logger, inventoryContext)
    {
        _context = inventoryContext;
    }

    public async Task UpdateTimespan(string id)
    {
        var thread = await _context.Posts.FindAsync(id);
        thread.TimeStamp = DateTime.Now;
        await Update(thread);
        await _context.SaveChangesAsync();
    }
}
