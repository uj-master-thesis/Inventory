using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries;

public class GetThreadWithPostsHandler : IRequestHandler<GetThreadWithPostsQuery, GetThreadByIdWithPostsResponse>
{
    ILogger<GetThreadWithPostsHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadThreadRepository _repository;
    public GetThreadWithPostsHandler(ILogger<GetThreadWithPostsHandler> logger, IMapper mapper, IReadThreadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    async Task<GetThreadByIdWithPostsResponse> IRequestHandler<GetThreadWithPostsQuery, GetThreadByIdWithPostsResponse>.Handle(GetThreadWithPostsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        var thread = await _repository.GetThreadWithPosts(request.ThreadId);
        thread.Posts ??= new List<Domain.Model.Post>(); 
        return  _mapper.Map<GetThreadByIdWithPostsResponse>(thread);
    }
}
