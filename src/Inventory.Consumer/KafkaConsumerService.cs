using Confluent.Kafka;
using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using Inventory.Application.Commands.Comment.AddCommentCommand;
using Inventory.Application.Commands.Vote;
using Inventory.Consumer.CircuitBreaker;
using Inventory.Consumer.Model;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Inventory.Consumer;

internal class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IMediator _mediator;
    private readonly IConsumer<string, string> _consumer;
    private readonly AsyncCircuitBreakerPolicy _circuitBreaker; 
    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IMediator mediator, 
        IConsumer<string, string> consumer, 
        ICircuitBreakerFactory circuitBreakerFactory,
        KafkaConfiguration kafkaConfiguration)
    {
        _logger = logger;
        _mediator = mediator;
        _consumer = consumer;
        _circuitBreaker = circuitBreakerFactory.Create(); 
        _consumer.Subscribe(new List<string> { kafkaConfiguration.TopicName }); 
    }
    private const string AddThreadCommandKey = "name";
    private const string AddPostCommandKey = "threadName";
    private const string AddCommentCommandKey = "text";
    private const string AddVoteCommandKey = "voteType";

    protected override  Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return ConsumeMessage(stoppingToken); 
    }

    private async Task ConsumeMessage(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start consumig events from kafka");
        await Task.Delay(1000);
        ConsumeResult<string, string> mesageToConsume = null;
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                mesageToConsume = GetMessageToProcces(mesageToConsume);
                _logger.LogInformation($"Processing event. EventId: {mesageToConsume.Message.Key}");
                await _mediator.Send(GetCommand(mesageToConsume.Message));
            }
            catch (BrokenCircuitException brkEx)
            {
                _logger.LogError(brkEx, $"Circuit was open. EventId: {mesageToConsume?.Message.Key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed consumming event. EventId: {mesageToConsume?.Message.Key}");
            }
        }
        _consumer.Close();
    }

    private static IRequest GetCommand(Message<string, string> message)
    {
        var command = JsonSerializer.Deserialize<JsonObject>(message.Value) ?? throw new ArgumentNullException();
        var settings = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        return command switch
        {
            var c when c.ContainsKey(AddThreadCommandKey) => JsonSerializer.Deserialize<AddThreadCommand>(command, settings),
            var c when c.ContainsKey(AddPostCommandKey) => JsonSerializer.Deserialize<AddPostCommand>(command, settings),
            var c when c.ContainsKey(AddCommentCommandKey) => JsonSerializer.Deserialize<AddCommentCommand>(command, settings),
            var c when c.ContainsKey(AddVoteCommandKey) => JsonSerializer.Deserialize<AddVoteCommand>(command, settings),
            _ => throw new NotImplementedException()
        }; 
    } 
    
    private ConsumeResult<string, string> GetMessageToProcces(ConsumeResult<string, string> prvMessage)
    {
        //if (_circuitBreaker.CircuitState == CircuitState.Open)
        //{
        //    return prvMessage; 
        //}
        return _consumer.Consume(); 
    }
}
