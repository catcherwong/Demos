namespace SerilogDemo.Controllers
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
            this._logger = loggerFactory.CreateLogger<ValuesController>();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("aaa");

            return new string[] { "value1", "value2" };
        }
    }
}
