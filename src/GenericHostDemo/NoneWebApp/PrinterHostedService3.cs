using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoneWebApp
{
    public class PrinterHostedService3 : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;
        private bool _stopping;
        private Task _backgroundTask;

        public PrinterHostedService3(ILoggerFactory loggerFactory, IOptionsSnapshot<AppSettings> options)
        {
            this._logger = loggerFactory.CreateLogger<PrinterHostedService3>();
            this._settings = options.Value;
        }
               
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Printer3 is starting.");
            _backgroundTask = BackgroundTask(cancellationToken);
            return Task.CompletedTask;
        }

        private async Task BackgroundTask(CancellationToken cancellationToken)
        {
            while (!_stopping)
            {
                await Task.Delay(TimeSpan.FromSeconds(_settings.PrinterDelaySecond),cancellationToken);
                Console.WriteLine("Printer3 is doing background work.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Printer3 is stopping.");
            _stopping = true;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.WriteLine("Printer3 is disposing.");
        }
    }
}
