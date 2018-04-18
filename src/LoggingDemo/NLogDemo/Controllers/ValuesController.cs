namespace NLogDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;

        public ValuesController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ValuesController>();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogDebug("debug");
            _logger.LogError("error");
            _logger.LogTrace("trace");
            _logger.LogInformation("info");
            _logger.LogWarning("warn");
            _logger.LogCritical("critical");

            return new string[] { "value1", "value2" };
        }
    }
}
