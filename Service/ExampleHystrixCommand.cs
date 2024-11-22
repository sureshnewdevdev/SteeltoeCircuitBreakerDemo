using Steeltoe.CircuitBreaker.Hystrix;
using System;
using System.Threading.Tasks;

namespace SteeltoeCircuitBreakerDemo.Service
{
    public class ExampleHystrixCommand : HystrixCommand<string>
    {
        private readonly IExampleService _exampleService;

        public ExampleHystrixCommand(IHystrixCommandOptions options, IExampleService exampleService)
            : base(options)
        {
            _exampleService = exampleService ?? throw new ArgumentNullException(nameof(exampleService));
        }

        protected override async Task<string> RunAsync()
        {
            // Simulate a failure
            throw new Exception("Simulated Failure");
        }

        protected override Task<string> RunFallbackAsync()
        {
            return Task.FromResult("Fallback response: Service is currently unavailable.");
        }
    }
}
