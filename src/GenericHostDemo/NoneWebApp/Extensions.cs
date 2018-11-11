using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NoneWebApp
{
    public static class Extensions
    {
        public static IHostBuilder UseHostedService<T>(this IHostBuilder hostBuilder)
            where T : class, IHostedService, IDisposable
        {
            return hostBuilder.ConfigureServices(services =>
                services.AddHostedService<T>());
        }

        public static IHostBuilder UseComsumeRabbitMQ(this IHostBuilder hostBuilder)            
        {
            return hostBuilder.ConfigureServices(services =>
                     services.AddHostedService<ComsumeRabbitMQHostedService>());
        }

        public static IHostBuilder UseTimer(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
                     services.AddHostedService<TimerHostedService>());
        }

        public static IHostBuilder UsePrinter(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
                     services.AddHostedService<PrinterHostedService2>());
        }
    }
}
