using Inventory.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Queries.GetThreadByIdQuery;

public class GetThreadByIdQueryHandler : IRequestHandler<GetThreadByIdQuery, GetThreadByIdResponse>
{
    ILogger<GetThreadByIdQueryHandler> _logger;
    public GetThreadByIdQueryHandler(ILogger<GetThreadByIdQueryHandler> logger)
    {
        _logger = logger;
    }

    Task<GetThreadByIdResponse> IRequestHandler<GetThreadByIdQuery, GetThreadByIdResponse>.Handle(GetThreadByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing query");
        return Task.FromResult(new GetThreadByIdResponse() { Id = request.Id, Description = "Fake Thread" }); 
    }
}
