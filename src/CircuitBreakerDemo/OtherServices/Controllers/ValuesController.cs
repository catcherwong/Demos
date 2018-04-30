namespace OtherServices.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OtherServices.Services.Commands;
    using System.Threading.Tasks;
    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/333
        [HttpGet]
        public async Task<string> Get([FromServices] GetOrderDetailsHystrixCommand command, string id = "0")
        {
            return await command.GetOrderDetailsAsync(id);
        }

        //[HttpGet]
        //public async Task<string> Get([FromServices] Services.IOrderService service, string id = "0")
        //{
        //    return await service.GetOrderDetailsAsync(id);
        //}
    }
}
