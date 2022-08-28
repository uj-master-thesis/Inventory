using Microsoft.Data.SqlClient;
using System.Data;

namespace Inventory.Infractracture.DbConfiguration.Dapper;

internal class InventoryReadContext
{
    private readonly string _connectionString;
    public InventoryReadContext(DatabaseConfiguration configuration)
    {
        _connectionString = configuration.ConectionString; 
    }
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
