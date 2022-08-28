
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;

namespace Inventory.Consumer.CircuitBreaker;

internal class CircuitBreakerFactory : ICircuitBreakerFactory
{
    private readonly ILogger<CircuitBreakerFactory> _logger; 

    public CircuitBreakerFactory(ILogger<CircuitBreakerFactory> logger)
    {
        _logger = logger; 
    }

    public AsyncCircuitBreakerPolicy Create()
    {
        return Policy.Handle<Exception>()
                     .CircuitBreakerAsync(1, TimeSpan.FromSeconds(20),
                      onBreak: (_, _) => _logger.LogWarning("Circuit cut, requests will not flow"),
                      onReset: () => _logger.LogWarning("Circuit closed, requests flow normally."),
                      onHalfOpen: () => _logger.LogWarning("Circuit closed, requests flow normally."));
    }
}
