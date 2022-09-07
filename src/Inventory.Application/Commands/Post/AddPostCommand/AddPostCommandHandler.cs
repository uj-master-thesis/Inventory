using AutoMapper;
using Inventory.Application.Commands.Thread.UpdatePostTimestampCommand;
using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.AddPostCommand;

public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Unit>
{
    private readonly ILogger<AddPostCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPostWriteRepository _writeRepository; 
    private readonly IMediator _mediator;

    public AddPostCommandHandler(ILogger<AddPostCommandHandler> logger, IMapper mapper, IPostWriteRepository writeRepository, IMediator mediator)
    {
        _logger = logger; 
        _mapper = mapper;
        _writeRepository = writeRepository;
        _mediator = mediator; 
    }

    public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command. Add post {request.PostName}");
        var post = _mapper.Map<Post>(request);
        await _writeRepository.Add(post);
        await _mediator.Send(new UpdateThreadTimestampCommand() { Name = request.ThreadName }); 
        return new Unit(); 
    }
}
