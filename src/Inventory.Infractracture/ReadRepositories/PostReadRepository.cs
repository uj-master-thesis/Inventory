using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class PostReadRepository : ReadBaseRepository<Post>, IReadPostRepository
{
    public PostReadRepository(ILogger<ReadBaseRepository<Post>> logger, InventoryReadContext context) : base(logger, context)
    {
    }
    protected override string QueryStringAll => "SELECT * SELECT Posts";
    protected override string QueryStringFirst => "SELECT * FROM Posts WHERE Id = @Id";

    public async Task<List<Post>> GetPostsByUser(string email)
    {
        return await QueryList("SELECT * FROM Posts WHERE Email = @Email", new[] { email });
    }
}
