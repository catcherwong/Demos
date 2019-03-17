namespace OrderApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly OrderDbContext _dbContext;

        public OrdersController(OrderDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var orders = _dbContext.Orders.ToList();
            return new string[] { "value1", "value2" };
        }

        // GET: api/orders/1
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            throw new System.Exception("Error Test");
            return "value";
        }
    }
}
