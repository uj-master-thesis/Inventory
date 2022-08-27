using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.UpdatePostCommand;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
{
    private readonly ILogger<UpdatePostCommandHandler> _logger;

    public UpdatePostCommandHandler(ILogger<UpdatePostCommandHandler> logger)
    {
        _logger = logger; 
    }

    public Task<Unit> Handle(UpdatePostCommand  request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command. Update post {request.Id}");
        return Task.FromResult(new Unit()); 
    }
}
