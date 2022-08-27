using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inventory.Infractracture.DbConfiguration.EntityFramework;

internal class InventoryContextFactory : IDesignTimeDbContextFactory<InventoryWriteContext>
{
    public InventoryWriteContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<InventoryWriteContext>();

        builder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;", _ => { });
        return new InventoryWriteContext(builder.Options);
    }
}
