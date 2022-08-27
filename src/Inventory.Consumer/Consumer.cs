using Confluent.Kafka.DependencyInjection;
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
        services.AddMediatR(Assembly.GetExecutingAssembly());                                                                                                               
        services.AddHostedService<KafkaConsumerService>();                                                       
    }                                                      
}                                                                                