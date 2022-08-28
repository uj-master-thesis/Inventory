
using Polly.CircuitBreaker;

namespace Inventory.Consumer.CircuitBreaker;

internal interface  ICircuitBreakerFactory
{
    AsyncCircuitBreakerPolicy Create(); 
}
