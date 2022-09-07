using MediatR;

namespace Inventory.Application.Commands.AddThreadCommand;

public class AddThreadCommand : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

