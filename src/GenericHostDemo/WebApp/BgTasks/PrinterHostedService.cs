using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApp.BgTasks
{
    public class PrinterHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;

        public PrinterHostedService(ILoggerFactory loggerFactory, IOptionsSnapshot<AppSettings> options)
        {
            this._logger = loggerFactory.CreateLogger<PrinterHostedService>();
            this._settings = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Printer is working.");
                await Task.Delay(TimeSpan.FromSeconds(_settings.PrinterDelaySecond), cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Printer is stopped");
            return Task.CompletedTask;
        }
    }      
}
