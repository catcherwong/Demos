namespace OrderServices.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/666
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return $"order-{id}";
        }
    }
}
