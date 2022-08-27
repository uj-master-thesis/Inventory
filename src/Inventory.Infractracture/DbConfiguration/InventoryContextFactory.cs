
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inventory.Infractracture.DbConfiguration;

internal class InventoryContextFactory : IDesignTimeDbContextFactory<InventoryContext>
{
    public InventoryContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<InventoryContext>();

        builder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;", _ => { });
        return new InventoryContext(builder.Options); 
    }
}
