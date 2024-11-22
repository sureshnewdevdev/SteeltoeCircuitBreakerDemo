using Microsoft.AspNetCore.Mvc;
using SteeltoeCircuitBreakerDemo.Service;
using System.Threading.Tasks;

namespace SteeltoeCircuitBreakerDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleHystrixCommand _hystrixCommand;

        public ExampleController(ExampleHystrixCommand hystrixCommand)
        {
            _hystrixCommand = hystrixCommand;
        }

        [HttpGet("get-data")]
        public async Task<IActionResult> GetData()
        {
            var response = await _hystrixCommand.ExecuteAsync();
            return Ok(response);
        }
    }
}
