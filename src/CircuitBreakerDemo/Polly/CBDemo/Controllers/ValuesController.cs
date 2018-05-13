using System.Threading.Tasks;
using CBDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CBDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<string> A([FromServices]IAService aService)
        {
            return await aService.GetAsync();
        }
    }
}
