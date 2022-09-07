using MediatR;

namespace Inventory.Application.Commands.Thread.UpdatePostTimestampCommand;

public class UpdateThreadTimestampCommand : IRequest
{
    public string Name { get; set; }
}
