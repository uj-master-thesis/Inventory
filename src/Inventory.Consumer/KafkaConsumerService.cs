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
    private readonly ISenderMessage _sender; 
    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IMediator mediator, 
        IConsumer<string, string> consumer, 
        ICircuitBreakerFactory circuitBreakerFactory,
        ISenderMessage sender,
        KafkaConfiguration kafkaConfiguration)
    {
        _logger = logger;
        _mediator = mediator;
        _consumer = consumer;
        _circuitBreaker = circuitBreakerFactory.Create();
        _sender = sender; 
        _consumer.Subscribe(new List<string> { kafkaConfiguration.TopicName }); 
    }

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
                await _sender.SendAsync(mesageToConsume.Message.Value);
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
    
    private ConsumeResult<string, string> GetMessageToProcces(ConsumeResult<string, string> prvMessage)
    {
        //if (_circuitBreaker.CircuitState == CircuitState.Open)
        //{
        //    return prvMessage; 
        //}
        return _consumer.Consume(); 
    }
}
