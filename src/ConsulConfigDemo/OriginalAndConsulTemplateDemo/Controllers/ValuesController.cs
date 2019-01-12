namespace OriginalAndConsulTemplateDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DemoAppSettings _options;
        private readonly DemoAppSettings _optionsSnapshot;

        public ValuesController(IConfiguration configuration, IOptions<DemoAppSettings> options, IOptionsSnapshot<DemoAppSettings> optionsSnapshot)
        {
            this._configuration = configuration;
            this._options = options.Value;
            this._optionsSnapshot = optionsSnapshot.Value;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { _configuration["DemoAppSettings:Key1"], _options.Key1, _optionsSnapshot.Key1 };
        }        
    }
}
