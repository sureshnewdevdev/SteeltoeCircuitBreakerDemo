using Microsoft.AspNetCore.Mvc;
using SteeltoeCircuitBreakerDemo.Service;

namespace SteeltoeCircuitBreakerDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ExampleHystrixCommand _hystrixCommand;

        public TestController(ExampleHystrixCommand hystrixCommand)
        {
            _hystrixCommand = hystrixCommand;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var response = await _hystrixCommand.ExecuteAsync();
            return Ok(response);
        }
    }

}
