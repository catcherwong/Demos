namespace StudentServices.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/school
        [HttpGet]
        [Route("school")]
        public IEnumerable<string> GetBySchool(string name)
        {
            return new string[] { $"{name}-value1", $"{name}-value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return $"Name-{id}";
        }
    }
}
