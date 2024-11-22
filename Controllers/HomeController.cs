using Microsoft.AspNetCore.Mvc;
using SteeltoeCircuitBreakerDemo.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteeltoeCircuitBreakerDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CircuitBreakerTestController : ControllerBase
    {
        private readonly ExampleHystrixCommand _hystrixCommand;

        public CircuitBreakerTestController(ExampleHystrixCommand hystrixCommand)
        {
            _hystrixCommand = hystrixCommand;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestCircuitBreaker()
        {
            var results = new List<string>();
            for (int i = 1; i <= 20; i++)
            {
                try
                {
                    // Simulate sending a request through the circuit breaker
                    var response = await _hystrixCommand.ExecuteAsync();
                    results.Add($"Request {i}: {response}");
                }
                catch (Exception ex)
                {
                    results.Add($"Request {i}: Circuit Breaker Activated - {ex.Message}");
                }

                // Optionally add a delay between requests to observe circuit behavior over time
                await Task.Delay(500); // 500ms delay between requests
            }

            return Ok(results);
        }
    }
}
