namespace SteeltoeCircuitBreakerDemo.Service
{
    public interface IExampleService
    {
        Task<string> GetExternalDataAsync();
    }
}
