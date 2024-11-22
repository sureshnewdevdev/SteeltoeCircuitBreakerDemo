using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CircuitBreaker.Hystrix;
using SteeltoeCircuitBreakerDemo.Service;

var builder = WebApplication.CreateBuilder(args);

// Configure HystrixCommandOptions explicitly
builder.Services.AddSingleton(sp =>
{
    var configSection = builder.Configuration.GetSection("Hystrix:Command:ExampleCommand");
    return new HystrixCommandOptions(
        HystrixCommandGroupKeyDefault.AsKey("ExampleGroup"),
        HystrixCommandKeyDefault.AsKey("ExampleCommand"))
    {
        ExecutionTimeoutInMilliseconds = configSection.GetValue<int>("ExecutionTimeoutInMilliseconds"),
        CircuitBreakerRequestVolumeThreshold = configSection.GetValue<int>("CircuitBreakerRequestVolumeThreshold"),
        CircuitBreakerErrorThresholdPercentage = configSection.GetValue<int>("CircuitBreakerErrorThresholdPercentage"),
        CircuitBreakerSleepWindowInMilliseconds = configSection.GetValue<int>("CircuitBreakerSleepWindowInMilliseconds")
    };
});

// Register ExampleService and HTTP Client
builder.Services.AddHttpClient<IExampleService, ExampleService>();

// Register a factory for ExampleHystrixCommand
builder.Services.AddScoped<ExampleHystrixCommand>(sp =>
{
    var options = sp.GetRequiredService<HystrixCommandOptions>();
    var exampleService = sp.GetRequiredService<IExampleService>();
    return new ExampleHystrixCommand(options, exampleService);
});

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
