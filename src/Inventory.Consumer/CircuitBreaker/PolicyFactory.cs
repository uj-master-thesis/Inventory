
using Inventory.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;

namespace Inventory.Consumer.CircuitBreaker;

public class PolicyFactory : IPolicyFactory
{
    private readonly ILogger<PolicyFactory> _logger; 

    public PolicyFactory(ILogger<PolicyFactory> logger)
    {
        _logger = logger; 
    }

    public AsyncCircuitBreakerPolicy CreateCircuitBreaker()
    {
        return Policy.Handle<DBAccesException>()
                     .CircuitBreakerAsync(1, TimeSpan.FromSeconds(20),
                      onBreak: (_, _) => _logger.LogWarning("Circuit cut, requests will not flow"),
                      onReset: () => _logger.LogWarning("Circuit closed, requests flow normally."),
                      onHalfOpen: () => _logger.LogWarning("Circuit closed, requests flow normally."));
    }

    public Policy CreateRetryAsync()
    {
        return Policy.Handle<DBAccesException>()
                    .WaitAndRetry(retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retry {retryCount} of inserting value to db", LogLevel.Error, exception);

                    });
    }
}
