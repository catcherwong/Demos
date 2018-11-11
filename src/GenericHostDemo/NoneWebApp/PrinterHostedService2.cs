using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoneWebApp
{
    public class PrinterHostedService2 : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;

        public PrinterHostedService2(ILoggerFactory loggerFactory, IOptionsSnapshot<AppSettings> options)
        {
            this._logger = loggerFactory.CreateLogger<PrinterHostedService2>();
            this._settings = options.Value;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Printer2 is stopped");
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Printer2 is working.");
                await Task.Delay(TimeSpan.FromSeconds(_settings.PrinterDelaySecond), stoppingToken);
            }
        }
    }
}
