using Dapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Inventory.Infractracture.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class PostReadRepository : ReadBaseRepository<Post>, IReadPostRepository
{
    private readonly InventoryReadContext _context;
    private readonly InventoryWriteContext _contextTwo; 
    public PostReadRepository(ILogger<ReadBaseRepository<Post>> logger, InventoryReadContext context, InventoryWriteContext context1) : base(logger, context)
    {
        _context = context;
        _contextTwo = context1; 
    }
    protected override string QueryStringAll => "SELECT * FROM Posts";
    protected override string QueryStringFirst => $"{QueryStringAll} WHERE Id = @Id";

    private string InnerJoinComments => "LEFT JOIN  Comments as Co on Posts.Id = Co.PostId"; 
    public async Task<List<Post>> GetPostsByUser(string email)
    {
        return await _contextTwo.Posts.Where(w => w.UserName == email).ToListAsync();
    }

    public async override Task<List<Post>> GetAll()
    {
        return await _contextTwo.Posts.Include(w => w.Comments).ToListAsync(); 
    }

    public async override Task<Post> Get(string id)
    {
        return await _contextTwo.Posts.Include(w => w.Comments).Where(w => w.Name == id).FirstOrDefaultAsync();
    }
}
