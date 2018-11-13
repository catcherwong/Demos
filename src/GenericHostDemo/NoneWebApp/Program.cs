using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;

namespace NoneWebApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                //logging
                .ConfigureLogging(factory =>
                {
                    //use nlog
                    factory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    NLog.LogManager.LoadConfiguration("nlog.config");
                })
                //host config
                .ConfigureHostConfiguration(config =>
                {
                    //command line
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                //app config
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = hostContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
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
                .UseHostedService<PrinterHostedService3>()
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
