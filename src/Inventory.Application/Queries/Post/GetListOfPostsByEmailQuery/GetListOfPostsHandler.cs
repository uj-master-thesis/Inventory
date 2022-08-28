using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.Post;

public class GetListOfPostsByEmailHandler : IRequestHandler<GetListOfPostsByEmailQuery, List<GetPostByIdResponse>>
{
    ILogger<GetListOfPostsByEmailHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadPostRepository _repository;
    public GetListOfPostsByEmailHandler(ILogger<GetListOfPostsByEmailHandler> logger, IMapper mapper, IReadPostRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<List<GetPostByIdResponse>> Handle(GetListOfPostsByEmailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        var posts =  await _repository.GetPostsByUser(request.Email);
        return posts.Select(p => _mapper.Map<GetPostByIdResponse>(p)).ToList();
    }
}
