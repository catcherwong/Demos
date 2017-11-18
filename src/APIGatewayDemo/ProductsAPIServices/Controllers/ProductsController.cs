namespace ProductsAPIServices.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ProductsController : Controller
    {        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Surface Book 2", "Mac Book Pro" };
        }
    }
}
