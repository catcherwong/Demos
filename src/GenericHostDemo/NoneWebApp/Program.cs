using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NoneWebApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureLogging(factory =>
                {
                    factory.AddConsole();
                })
                  .ConfigureAppConfiguration((hostContext, config) =>
                  {
                      config.AddEnvironmentVariables();
                      config.AddJsonFile("appsettings.json", optional: true);
                      config.AddCommandLine(args);
                  })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));

                    //basic usage

                    //this service can not stop by control+c
                    //services.AddHostedService<PrinterHostedService>();
                    //services.AddHostedService<PrinterHostedService2>();
                    //services.AddHostedService<TimerHostedService>();
                    //services.AddHostedService<ComsumeRabbitMQHostedService>();
                })
                //here use extensions
                .UseTimer()
                .UsePrinter()
                //.UseComsumeRabbitMQ()
                //.UseHostedService<TimerHostedService>()
                //.UseHostedService<PrinterHostedService2>()
                //.UseHostedService<ComsumeRabbitMQHostedService>()
                ;

            //console 
            await builder.RunConsoleAsync();

            ////start and wait for shutdown
            //var host = builder.Build();
            //using (host)
            //{
            //    await host.StartAsync();

            //    await host.WaitForShutdownAsync();
            //}
        }
    }
}
