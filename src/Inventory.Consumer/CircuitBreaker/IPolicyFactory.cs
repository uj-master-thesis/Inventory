
using Polly;
using Polly.CircuitBreaker;

namespace Inventory.Consumer.CircuitBreaker;

public interface  IPolicyFactory
{
    AsyncCircuitBreakerPolicy CreateCircuitBreaker();
    Policy CreateRetryAsync();

}
