namespace TestAPI.Controllers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static int myCount = 0;

        private readonly IHttpClientFactory _clientFactory;

        public ValuesController(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }

        // GET api/values/other
        [HttpGet("other")]
        public ActionResult<IEnumerable<string>> Other()
        {            
            Response.StatusCode = 408;

            return new string[] { "value1", "value2" };
        }

        // GET api/values/timeout
        [HttpGet("timeout")]
        public ActionResult<IEnumerable<string>> Timeout()
        {

            if (myCount < 3)
            {
                System.Threading.Thread.Sleep(3000);
            }

            myCount++;

            return new string[] { "value1", "value2" };
        }

        // GET api/values
        [HttpGet("")]
        public async Task<string> GetAsync()
        {
            var client = _clientFactory.CreateClient("cb");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/values/timeout");

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

    }
}
