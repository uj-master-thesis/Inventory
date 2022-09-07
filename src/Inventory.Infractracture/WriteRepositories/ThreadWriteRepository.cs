using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class ThreadWriteRepository : WriteBaseRepository<Domain.Model.Thread>, IThreadWriteRepository
{
    private readonly InventoryWriteContext _context;
    public ThreadWriteRepository(ILogger<WriteBaseRepository<Domain.Model.Thread>> logger, InventoryWriteContext inventoryContext) : base(logger, inventoryContext)
    {
        _context = inventoryContext;
    }

    public async Task UpdateTimespan(string id)
    {
        _context.ChangeTracker.Clear(); 

        var thread = await _context.Threads.FindAsync(id);
        thread.TimeStamp = DateTime.Now;
        await Update(thread); 
        _context.SaveChanges();
    }
}
