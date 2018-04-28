namespace SchoolServices.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Steeltoe.CircuitBreaker.Hystrix;

    public class StudentServiceHystrixCommand : HystrixCommand<string>
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentServiceHystrixCommand> _logger;
        private string _name;

        public StudentServiceHystrixCommand(
            IHystrixCommandOptions options,
            IStudentService service,
            ILogger<StudentServiceHystrixCommand> logger
            ) : base(options)
        {
            this._service = service;
            this._logger = logger;
            this.IsFallbackUserDefined = true;
        }

        public async Task<string> GetStudentListAsync(string name)
        {
            _name = name;
            return await ExecuteAsync();
        }

        protected override async Task<string> RunAsync()
        {
            var result = await _service.GetStudentListAsync(_name);
            _logger.LogInformation("Run: {0}", result);
            return result;
        }

        protected override async Task<string> RunFallbackAsync()
        {
            _logger.LogInformation("RunFallback");
            return await Task.FromResult<string>("RunFallbackAsync");
        }
    }
}
