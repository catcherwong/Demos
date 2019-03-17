namespace CustomerApi.Controllers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _factory;

        public ValuesController(IHttpClientFactory factory)
        {
            this._factory = factory;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            var data = await GetSomeThingFromOrderApi();

            return new string[] { "value1", "value2" };
        }

        private async Task<string> GetSomeThingFromOrderApi()
        {
            var client = _factory.CreateClient("orderApi");

            var requestMsg = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8990/api/orders");

            var responseMsg  = await client.SendAsync(requestMsg);

            var data = await responseMsg.Content.ReadAsStringAsync();

            return data;
        }
    }
}
