using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.DeletePostCommand;

public class AddThreadCommandHandler : IRequestHandler<DeletePostCommand, Unit>
{
    private readonly ILogger<DeletePostCommand> _logger;

    public AddThreadCommandHandler(ILogger<DeletePostCommand> logger)
    {
        _logger = logger; 
    }

    public Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command. Delete post {request.Id}");
        return Task.FromResult(new Unit()); 
    }
}
