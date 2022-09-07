using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.WriteRepositories;

internal class VoteWriteRepository : WriteBaseRepository<Domain.Model.Vote>, IVoteWriteRepository
{
    private readonly InventoryWriteContext _writeContext; 
    public VoteWriteRepository(ILogger<VoteWriteRepository> logger, InventoryWriteContext inventoryContext) : base(logger, inventoryContext)
    {
        _writeContext = inventoryContext; 
    }

    public override async Task<Vote> Add(Vote entity)
    {
        var vote = await _writeContext.Votes.FirstOrDefaultAsync(w => w.PostName == entity.PostName && w.UserName == entity.UserName);
        if (vote is null)
        {
            await _writeContext.Votes.AddAsync(entity); 
        }
        else if(vote.VoteType != entity.VoteType)
        {
            vote.VoteType = entity.VoteType; 
        }
        await _writeContext.SaveChangesAsync();
        await UpdatePostVotes(entity.PostName); 
        return vote ?? entity; 
    }

    private async Task UpdatePostVotes(string id)
    {
        var post = await _writeContext.Posts.FindAsync(id);
        post.UpVotes = await _writeContext.Votes.Where(w => w.PostName == id && w.VoteType == VoteType.UpVote).CountAsync(); 
        post.DownVotes = await _writeContext.Votes.Where(w => w.PostName == id && w.VoteType == VoteType.DownVote).CountAsync();
        _writeContext.Posts.Update(post);
        await _writeContext.SaveChangesAsync(); 
    }
}
