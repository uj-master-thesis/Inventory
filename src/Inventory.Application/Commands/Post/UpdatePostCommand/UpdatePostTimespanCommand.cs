using Inventory.Domain.Model;
using MediatR;

namespace Inventory.Application.Commands.UpdatePostCommand;

public class UpdatePostTimespanCommand : IRequest
{
    public string Name { get; set; }
}
