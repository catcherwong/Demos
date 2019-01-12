namespace DirectConsulApiDemo.Controllers
{
    using Consul;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConsulClient _consulClient;

        public ValuesController(IConsulClient consulClient)
        {
            this._consulClient = consulClient;
        }
       
        [HttpGet("")]
        public async Task<string> GetAsync([FromQuery]string key)
        {
            var str = string.Empty;
            var res = await _consulClient.KV.Get(key);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                str = System.Text.Encoding.UTF8.GetString(res.Response.Value);
            }

            return $"value-{str}";
        }
    }
}
