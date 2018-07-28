using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteeltoeWithHttpClientFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;

        public ValuesController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ValuesController>();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInformation("Get Here!!");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/text
        [HttpGet("text")]
        public async Task<string> GetTextAsync([FromServices]Services.IMyService service)
        {
            return await service.GetTextAsync();
        }
    }
}
