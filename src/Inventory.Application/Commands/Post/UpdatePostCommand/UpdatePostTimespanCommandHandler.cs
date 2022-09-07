using Inventory.Application.Interfaces.WriteRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.UpdatePostCommand;

public class UpdatePostTimespanCommandHandler : IRequestHandler<UpdatePostTimespanCommand, Unit>
{
    private readonly ILogger<UpdatePostTimespanCommandHandler> _logger;
    private readonly IPostWriteRepository _writeRepository;
    public UpdatePostTimespanCommandHandler(ILogger<UpdatePostTimespanCommandHandler> logger, IPostWriteRepository writeRepository)
    {
        _logger = logger; 
        _writeRepository = writeRepository;
    }

    public async Task<Unit> Handle(UpdatePostTimespanCommand  request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command. Update post timespan {request.Name}");
        await _writeRepository.UpdateTimespan(request.Name);
        return new Unit(); 
    }
}
