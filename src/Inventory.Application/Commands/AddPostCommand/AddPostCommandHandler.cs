using AutoMapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.AddPostCommand;

public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Unit>
{
    private readonly ILogger<AddPostCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPostWriteRepository _writeRepository; 

    public AddPostCommandHandler(ILogger<AddPostCommandHandler> logger, IMapper mapper, IPostWriteRepository writeRepository)
    {
        _logger = logger; 
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command. Add post {request.Id}");
        var post = _mapper.Map<Post>(request);
        await _writeRepository.Add(post);
        return new Unit(); 
    }
}
