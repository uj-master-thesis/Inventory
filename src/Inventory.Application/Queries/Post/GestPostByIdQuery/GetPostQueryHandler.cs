using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.GestPostQuery;

public class GetPostQueryHandler : IRequestHandler<GetPostByNameQuery, GetPostResponse>
{
    private readonly ILogger<GetPostQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadPostRepository _repository; 
    public GetPostQueryHandler(ILogger<GetPostQueryHandler> logger, IMapper mapper, IReadPostRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository; 
    }

    async Task<GetPostResponse> IRequestHandler<GetPostByNameQuery, GetPostResponse>.Handle(GetPostByNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        return _mapper.Map<GetPostResponse>(await _repository.Get(request.Name)); 
    }
}
