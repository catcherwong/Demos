using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "visit by jwt auth";
        }        
    }
}
