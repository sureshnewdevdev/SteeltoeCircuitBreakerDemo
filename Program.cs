using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CircuitBreaker.Hystrix.MetricsStream;
using Steeltoe.CircuitBreaker.Hystrix;
using SteeltoeCircuitBreakerDemo.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;





namespace SteeltoeCircuitBreakerDemo
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHystrixCommand<ExampleHystrixCommand>("ExampleCommand", builder.Configuration);
            builder.Services.AddHttpClient<IExampleService, ExampleService>();
            // Add services to the container.

            builder.Services.AddControllers();

            // Add Steeltoe Hystrix
         

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
  

            var app = builder.Build();

        

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Use Hystrix Dashboard (optional, for visualization)
            //app.UseHystrixMetricsStream();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
