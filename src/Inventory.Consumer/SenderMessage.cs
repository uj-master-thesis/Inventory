using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using Inventory.Application.Commands.Comment.AddCommentCommand;
using Inventory.Application.Commands.Vote;
using Inventory.Application.Exceptions;
using Inventory.Consumer.CircuitBreaker;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Inventory.Consumer;

public interface ISenderMessage
{
    public Task SendAsync(string message);
}

public class SenderMessage : ISenderMessage
{
    private readonly IMediator _mediator;
    private readonly ILogger<SenderMessage> _logger;
    private readonly Policy _policy; 
    private const string AddThreadCommandKey = "name";
    private const string AddPostCommandKey = "threadName";
    private const string AddCommentCommandKey = "text";
    private const string AddVoteCommandKey = "voteType";
    public SenderMessage(IMediator mediator, ILogger<SenderMessage> logger, IPolicyFactory _policyFactory)
    {
        _mediator = mediator;
        _logger = logger;
        _policy = _policyFactory.CreateRetryAsync(); 
    }
    public async Task SendAsync(string message)
    {
        await _policy.Execute(async() =>
                    {
                        _logger.LogInformation($"Send message to mediaR; {message}");
                        return await _mediator.Send(GetCommand(message));
                    });
    }

    private static IRequest GetCommand(string message)
    {
        var command = JsonSerializer.Deserialize<JsonObject>(message)["data"] ?? throw new ArgumentNullException();
        var settings = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        return command switch
        {
            var c when c.AsObject().ContainsKey(AddThreadCommandKey) => JsonSerializer.Deserialize<AddThreadCommand>(command, settings),
            var c when c.AsObject().ContainsKey(AddPostCommandKey) => JsonSerializer.Deserialize<AddPostCommand>(command, settings),
            var c when c.AsObject().ContainsKey(AddCommentCommandKey) => JsonSerializer.Deserialize<AddCommentCommand>(command, settings),
            var c when c.AsObject().ContainsKey(AddVoteCommandKey) => JsonSerializer.Deserialize<AddVoteCommand>(command, settings),
            _ => throw new NotImplementedException()
        };
    }


}
