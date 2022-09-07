using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.GetThreadByIdQuery;

public class GetThreadByIdQueryHandler : IRequestHandler<GetThreadByIdQuery, GetThreadByIdResponse>
{
    ILogger<GetThreadByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadThreadRepository _repository;
    public GetThreadByIdQueryHandler(ILogger<GetThreadByIdQueryHandler> logger, IMapper mapper, IReadThreadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    async Task<GetThreadByIdResponse> IRequestHandler<GetThreadByIdQuery, GetThreadByIdResponse>.Handle(GetThreadByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        return _mapper.Map<GetThreadByIdResponse>(await _repository.Get(""));
    }
}
