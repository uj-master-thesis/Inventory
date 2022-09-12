using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.EntityFrameworkCore;
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

    public async Task UpdatePostVotes(string id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(w => w.Name == id);
        post.UpVotes = await _context.Votes.Where(w => w.PostName == id && w.VoteType == VoteType.UpVote).CountAsync();
        post.DownVotes = await _context.Votes.Where(w => w.PostName == id && w.VoteType == VoteType.DownVote).CountAsync();
        await Update(post);
    }
}
