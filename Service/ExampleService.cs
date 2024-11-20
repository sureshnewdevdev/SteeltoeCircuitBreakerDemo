using System.Net.Http;
using System.Threading.Tasks;

namespace SteeltoeCircuitBreakerDemo.Service
{
    public class ExampleService : IExampleService
    {
        private readonly HttpClient _httpClient;

        public ExampleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetExternalDataAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");

            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        
    }
}
