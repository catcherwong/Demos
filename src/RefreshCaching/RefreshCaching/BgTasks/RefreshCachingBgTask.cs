namespace RefreshCaching
{
    using EasyCaching.Core;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class RefreshCachingBgTask : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IEasyCachingProviderFactory _providerFactory;        
        private Timer _timer;
        private bool _refreshing;
        
        public RefreshCachingBgTask(ILoggerFactory loggerFactory, IEasyCachingProviderFactory providerFactory)
        {            
            this._logger = loggerFactory.CreateLogger<RefreshCachingBgTask>();
            this._providerFactory = providerFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Refresh caching backgroud taks begin ...");
            
            _timer = new Timer(async x =>
            {
                if (_refreshing)
                {
                    _logger.LogInformation($"Latest manipulation is still working ...");
                    return;
                }
                _refreshing = true;
                await RefreshAsync();
                _refreshing = false;
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        private async Task RefreshAsync()
        {
            _logger.LogInformation($"Refresh caching begin at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            try
            {
                var cachingProvider = _providerFactory.GetCachingProvider("m1");

                // mock query data from database or others 
                var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var random = new Random().NextDouble();

                // only once
                var dict = new Dictionary<string, string>()
                {
                    { ConstValue.Time_Cache_Key, time },
                    { ConstValue.Random_Cache_Key, random.ToString() }
                };
                await cachingProvider.SetAllAsync(dict, TimeSpan.FromDays(30));

                //// one by one
                //await cachingProvider.SetAsync(Time_Cache_Key, time, TimeSpan.FromSeconds(10));
                //await cachingProvider.SetAsync(Random_Cache_Key, random.ToString(), TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Refresh caching error ...");                
            }

            _logger.LogInformation($"Refresh caching end at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Refresh caching backgroud taks end ...");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
