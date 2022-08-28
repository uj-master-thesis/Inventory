using Dapper;

namespace Inventory.Infractracture.Utils;

internal class DynamincParametersFactory
{
    public static DynamicParameters CreateFromArray((string key, string value)[] param)
    {
        var parameters = new DynamicParameters();
        foreach (var (key, value) in param)
        {
            parameters.Add(key, value); 
        }
        return parameters; 
    }
}
