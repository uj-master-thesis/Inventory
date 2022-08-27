using MediatR;

namespace Inventory.Application.Commands.DeletePostCommand;

public class DeletePostCommand : IRequest
{
    public Guid? Id {get; set; }
}
