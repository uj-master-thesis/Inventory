using Inventory.Domain.Model;
using MediatR;

namespace Inventory.Application.Commands.UpdatePostCommand;

public class UpdatePostCommand : Post, IRequest
{
}
