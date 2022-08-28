using System.Text.Json.Nodes;

namespace Inventory.Consumer.Model;

internal class CommandWrapper
{
    public CommandType CommandType { get; set; }
    public JsonObject CommandObject { get; set; }
}
