using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.GestPostQuery;

public class GetPostQueryHandler : IRequestHandler<GetPostByIdQuery, GetPostByIdResponse>
{
    ILogger<GetPostQueryHandler> _logger;
    public GetPostQueryHandler(ILogger<GetPostQueryHandler> logger)
    {
        _logger = logger;
    }

    Task<GetPostByIdResponse> IRequestHandler<GetPostByIdQuery, GetPostByIdResponse>.Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        return Task.FromResult(new GetPostByIdResponse() { Id = request.Id, PostName = "Fake post" }); 
    }
}
