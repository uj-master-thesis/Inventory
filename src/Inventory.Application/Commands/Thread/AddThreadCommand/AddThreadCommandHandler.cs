using AutoMapper;
using Inventory.Application.Interfaces.WriteRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.AddThreadCommand;

public class AddThreadCommandHandler : IRequestHandler<AddThreadCommand, Unit>
{
    private readonly ILogger<AddThreadCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IThreadWriteRepository _writeRepository;
    public AddThreadCommandHandler(ILogger<AddThreadCommandHandler> logger, IMapper mapper, IThreadWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Unit> Handle(AddThreadCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command: Add post {request.Name}");
        var thread = _mapper.Map<Domain.Model.Thread>(request);
        await _writeRepository.Add(thread);
        return new Unit();
    }
}
