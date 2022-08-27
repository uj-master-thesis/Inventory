using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.Thread.GetListOfPostsQuery;

public class GetListOfPostsHandler : IRequestHandler<GetListOfPostsQuery, List<GetPostByIdResponse>>
{
    ILogger<GetListOfPostsHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadPostRepository _repository;
    public GetListOfPostsHandler(ILogger<GetListOfPostsHandler> logger, IMapper mapper, IReadPostRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<List<GetPostByIdResponse>> Handle(GetListOfPostsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        var posts =  await _repository.GetAll();
        return posts.Select(p => _mapper.Map<GetPostByIdResponse>(p)).ToList();
    }
}
