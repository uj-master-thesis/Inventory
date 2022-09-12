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
            await base.Add(entity); 
        }
        else if(vote.VoteType != entity.VoteType)
        {
            vote.VoteType = entity.VoteType;
            await base.Update(vote);
        }
        return vote ?? entity; 
    }
}
