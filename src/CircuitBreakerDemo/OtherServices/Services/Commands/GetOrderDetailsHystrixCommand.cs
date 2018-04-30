namespace OtherServices.Services.Commands
{
    using Microsoft.Extensions.Logging;
    using Steeltoe.CircuitBreaker.Hystrix;
    using System.Threading.Tasks;
    
    public class GetOrderDetailsHystrixCommand : HystrixCommand<string>
    {
        private readonly IOrderService _service;
        private readonly ILogger<GetOrderDetailsHystrixCommand> _logger;
        private string _orderId;

        public GetOrderDetailsHystrixCommand(
            IHystrixCommandOptions options,
            IOrderService service,
            ILogger<GetOrderDetailsHystrixCommand> logger
            ) : base(options)
        {
            this._service = service;
            this._logger = logger;
            this.IsFallbackUserDefined = true;
        }

        public async Task<string> GetOrderDetailsAsync(string orderId)
        {
            _orderId = orderId;
            return await ExecuteAsync();
        }

        protected override async Task<string> RunAsync()
        {
            var result = await _service.GetOrderDetailsAsync(_orderId);
            _logger.LogInformation("Get the result : {0}", result);
            return result;
        }

        protected override async Task<string> RunFallbackAsync()
        {
            if (!this._circuitBreaker.AllowRequest)
            {
                return await Task.FromResult("Please wait for sometimes");
            }

            _logger.LogInformation($"RunFallback");
            return await Task.FromResult<string>($"RunFallbackAsync---OrderId={_orderId}");
        }

    }
}
