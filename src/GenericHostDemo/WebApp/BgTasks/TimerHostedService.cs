using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApp.BgTasks
{
    public class TimerHostedService : BackgroundService 
    {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;
        private Timer _timer;

        public TimerHostedService(ILoggerFactory loggerFactory, IOptionsSnapshot<AppSettings> options)
        {
            this._logger = loggerFactory.CreateLogger<TimerHostedService>();
            this._settings = options.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_settings.TimerPeriod));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timer is working");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timer is stopping");
            _timer?.Change(Timeout.Infinite, 0);

            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }
    }
}
