using Confluent.Kafka.DependencyInjection;
using Inventory.Consumer.CircuitBreaker;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Consumer;

public static class Consumer
{
    public static void ConfigureConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection(nameof(KafkaConfiguration)).Get<KafkaConfiguration>();

        services.AddKafkaClient(new Dictionary<string, string>
        {
            { "bootstrap.servers", config.Server },
            { "group.id", "group1" }
        });
        services.AddSingleton(config);
        services.AddSingleton<IPolicyFactory, PolicyFactory>();
        services.AddSingleton<ISenderMessage, SenderMessage>(); 
        services.AddMediatR(Assembly.GetExecutingAssembly());                                                                                                               
        services.AddHostedService<KafkaConsumerService>();                                                       
    }                                                      
}                                                                                