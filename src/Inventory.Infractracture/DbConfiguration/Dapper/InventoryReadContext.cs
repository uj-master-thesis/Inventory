using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
