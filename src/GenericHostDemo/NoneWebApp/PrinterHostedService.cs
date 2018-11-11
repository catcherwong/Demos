using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoneWebApp
{
    public class PrinterHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;

        public PrinterHostedService(ILoggerFactory loggerFactory, IOptionsSnapshot<AppSettings> options)
        {
            this._logger = loggerFactory.CreateLogger<PrinterHostedService>();
            this._settings = options.Value;
        }

        public void Dispose()
        {
            Console.WriteLine("game over");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Printer is working.");
                await Task.Delay(TimeSpan.FromSeconds(_settings.PrinterDelaySecond), cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Printer is stopped");
            return Task.CompletedTask;
        }
    }     
}
