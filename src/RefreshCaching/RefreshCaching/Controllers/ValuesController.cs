namespace RefreshCaching.Controllers
{
    using EasyCaching.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEasyCachingProviderFactory _providerFactory;        

        public ValuesController(IEasyCachingProviderFactory providerFactory)
        {
            this._providerFactory = providerFactory;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var provider = _providerFactory.GetCachingProvider("m1");

            var time = provider.Get<string>(ConstValue.Time_Cache_Key);

            // do somethings based on time ...

            var random = provider.Get<string>(ConstValue.Random_Cache_Key);

            // do somethings based on random ...


            return new string[] { time.Value, random.Value };            
        }        
    }
}
