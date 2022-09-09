using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class CommentReadRepository : ReadBaseRepository<Comment>, IReadCommentRepository
{
    private readonly InventoryWriteContext _inventoryWrite; 
    public CommentReadRepository(ILogger<ReadBaseRepository<Comment>> logger, InventoryReadContext context, InventoryWriteContext writeContext) : base(logger, context)
    {
        _inventoryWrite = writeContext; 
    }

    protected override string QueryStringAll => throw new NotImplementedException();

    protected override string QueryStringFirst => throw new NotImplementedException();

    public  Task<List<Comment>> GetListOfCommentsByPostName(string postName)
    {
        return _inventoryWrite.Comments.Where(w => w.PostName == postName).ToListAsync(); 
    }

    public Task<List<Comment>> GetListOfCommentsByUser(string user)
    {
        return _inventoryWrite.Comments.Where(w => w.UserName == user).ToListAsync();
    }
}
