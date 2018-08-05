namespace DemoAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        // GET: api/persons
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return new List<Person>
            {
                new Person{Id = 1 , Name = "catcher wong"},
                new Person{Id = 2 , Name = "james"}
            };
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return new Person { Id = id, Name = "name" };
        }

        // POST api/persons
        [HttpPost]
        public Person Post([FromBody]Person person)
        {
            if (person == null) return new Person();

            return new Person { Id = person.Id, Name = person.Name };
        }

        // PUT api/persons/5
        [HttpPut()]
        public string Put([FromBody]int id)
        {
            return $"put {id}";
        }

        // DELETE api/persons/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"del {id}";
        }

        static int trCount = 0;
        [HttpGet("tr")]
        public string TimeOutR()
        {
            trCount++;
            if(trCount < 2)
            {
                throw new Exception("11");
            }

            return "timeout";
        }

        static int twCount = 0;
        [HttpGet("tw")]
        public string TimeOutW()
        {
            twCount++;
            if (twCount < 2)
            {
                throw new Exception("11");
            }
            return "timeout";
        }
    }
}
