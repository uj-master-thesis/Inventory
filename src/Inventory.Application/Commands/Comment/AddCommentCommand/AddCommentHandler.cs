
using AutoMapper;
using Inventory.Application.Commands.UpdatePostCommand;
using Inventory.Application.Interfaces.WriteRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.Comment.AddCommentCommand;

public class AddCommentHandler : IRequestHandler<AddCommentCommand, Unit>
{
    private readonly ILogger<AddCommentHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICommentWriteRepository _writeRepository;
    private readonly IMediator _mediator;
    public AddCommentHandler(ILogger<AddCommentHandler> logger, IMapper mapper, ICommentWriteRepository writeRepository, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _mediator = mediator;
    }
    public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command: Add post");
        if (string.IsNullOrEmpty(request.Text)) return new Unit(); 
        var thread = _mapper.Map<Domain.Model.Comment>(request);
        await _writeRepository.Add(thread);
        await _mediator.Send(new UpdatePostTimespanCommand() { Name = request.PostName}); 
        return new Unit();
    }
}
