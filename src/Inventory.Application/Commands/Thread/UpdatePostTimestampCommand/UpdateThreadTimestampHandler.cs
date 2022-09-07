using Inventory.Application.Interfaces.WriteRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.Thread.UpdatePostTimestampCommand;

public class UpdateThreadTimestampHandler : IRequestHandler<UpdateThreadTimestampCommand, Unit>
{
    private readonly ILogger<UpdateThreadTimestampHandler> _logger;
    private readonly IThreadWriteRepository _writeRepository;

    public UpdateThreadTimestampHandler(ILogger<UpdateThreadTimestampHandler> logger, IThreadWriteRepository writeRepository)
    {
        _logger = logger;
        _writeRepository = writeRepository;
    }
    public async Task<Unit> Handle(UpdateThreadTimestampCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command: Update post {request.Name}");
        await _writeRepository.UpdateTimespan(request.Name);
        return new Unit();
    }
}
