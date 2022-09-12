
using AutoMapper;
using Inventory.Application.Interfaces.WriteRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Commands.Vote;

public class AddVoteCommandHandler : IRequestHandler<AddVoteCommand, Unit>
{
    private readonly ILogger<AddVoteCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IVoteWriteRepository _writeRepository;
    private readonly IPostWriteRepository _postRepository; 
    public AddVoteCommandHandler(ILogger<AddVoteCommandHandler> logger, IMapper mapper, IVoteWriteRepository writeRepository, IPostWriteRepository postRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(AddVoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing command: Add vote");
        var vote = _mapper.Map<Domain.Model.Vote>(request);
        await _writeRepository.Add(vote);
        await _postRepository.UpdatePostVotes(vote.PostName); 
        return new Unit();
    }
}
