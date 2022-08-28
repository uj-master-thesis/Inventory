using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Domain.Model;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.Utils;
using Microsoft.Extensions.Logging;

namespace Inventory.Infractracture.ReadRepositories;

internal class PostReadRepository : ReadBaseRepository<Post>, IReadPostRepository
{
    public PostReadRepository(ILogger<ReadBaseRepository<Post>> logger, InventoryReadContext context) : base(logger, context)
    {
    }
    protected override string QueryStringAll => "SELECT * FROM Posts";
    protected override string QueryStringFirst => $"{QueryStringAll} WHERE Id = @Id";

    public async Task<List<Post>> GetPostsByUser(string email)
    {
        return await QueryList($"{QueryStringAll} WHERE Email = @Email", DynamincParametersFactory.CreateFromArray(new[] { ("@Email", email) }));
    }
}
