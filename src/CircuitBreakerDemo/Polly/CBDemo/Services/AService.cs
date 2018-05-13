namespace CBDemo.Services
{
    using Microsoft.Extensions.Logging;
    using Polly;
    using Polly.Wrap;  
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IAService
    {
        Task<string> GetAsync();
    }

    public class AService : IAService
    {
        private PolicyWrap<string> _policyWrap;

        private ILogger _logger;

        public AService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AService>();

            var timeout = Policy
                  .TimeoutAsync(1, Polly.Timeout.TimeoutStrategy.Pessimistic, (context, ts, task) =>
                  {
                      _logger.LogInformation("AService timeout");
                      return Task.CompletedTask;
                  });

            var circuitBreaker = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(5), (ex, ts) =>
                {
                    _logger.LogInformation($"AService OnBreak -- ts = {ts.Seconds}s ,ex.message = {ex.Message}");
                }, () =>
                {
                    _logger.LogInformation("AService OnReset");
                });

            _policyWrap = Policy<string>
                .Handle<Exception>()
                .FallbackAsync(GetFallback(), (x) =>
                {
                    _logger.LogInformation($"AService Fallback -- {x.Exception.Message}");                    
                    return Task.CompletedTask;
                })
                .WrapAsync(circuitBreaker)
                .WrapAsync(timeout);
        }

        private string GetFallback()
        {
            return "fallback";
        }

        public async Task<string> GetAsync()
        {
            return await _policyWrap.ExecuteAsync(() =>
            {
                return QueryAsync();
            });
        }

        private async Task<string> QueryAsync()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetStringAsync("http://localhost:9001/api/values");
                return res;
            }
        }

    }
}
