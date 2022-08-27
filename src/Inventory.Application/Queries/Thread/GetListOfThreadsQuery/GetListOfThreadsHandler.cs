using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.Thread.GetListOfThreadsQuery;

public class GetListOfThreadsQueryHandler : IRequestHandler<GetListOfThreadsQuery, List<GetThreadByIdResponse>>
{
    ILogger<GetListOfThreadsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadThreadRepository _repository;
    public GetListOfThreadsQueryHandler(ILogger<GetListOfThreadsQueryHandler> logger, IMapper mapper, IReadThreadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<List<GetThreadByIdResponse>> Handle(GetListOfThreadsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        var posts = await _repository.GetAll();
        return posts.Select(p => _mapper.Map<GetThreadByIdResponse>(p)).ToList();
    }
}
