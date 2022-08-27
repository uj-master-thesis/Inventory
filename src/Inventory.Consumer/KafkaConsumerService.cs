using Confluent.Kafka;
using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Inventory.Consumer;

internal class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IMediator _mediator;
    private readonly IConsumer<string, string> _consumer; 
    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IMediator mediator, 
        IConsumer<string, string> consumer , 
        KafkaConfiguration kafkaConfiguration)
    {
        _logger = logger;
        _mediator = mediator;
        _consumer = consumer;
        _consumer.Subscribe(new List<string> { kafkaConfiguration.TopicName }); 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start consumig events from kafka");
        await Task.Delay(1000);
        ConsumeResult<string, string> result = null;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                result = _consumer.Consume();
                _logger.LogInformation($"Processing event. EventId: {result.Message.Key}");
                await _mediator.Send(GetAddPostCommand(result.Message));
            }
            catch (Exception ex)                                                                                                    
            {
                _logger.LogError(ex, $"Failed consumming event. EventId: {result?.Message.Key}");
            }
        }
        _consumer.Close();
    }

    private AddThreadCommand GetAddPostCommand(Message<string, string> message)
    {
        var addPostCommand = JsonSerializer.Deserialize<AddThreadCommand>(message.Value) ?? throw new ArgumentNullException();
        addPostCommand.Id = Guid.Parse(message.Key);       
        return addPostCommand;                                                                  
    }                             
}
