namespace RefitClientApi.Controllers
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IPersonsApi _api;

        public ValuesController(IPersonsApi api)
        {
            this._api = api;
        }

        // GET api/values
        [HttpGet]
        public async Task<List<Person>> GetAsync()
        {
            return await _api.GetPersonsAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Person> GetAsync(int id)
        {
            return await _api.GetPersonAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<Person> PostAsync([FromBody] Person value)
        {
            return await _api.AddPersonAsync(value);
        }

        // PUT api/values
        [HttpPut]
        public async Task<string> PutAsync([FromBody]int id)
        {
            return await _api.EditPersonAsync(id);
        }

        // DELETE api/values
        [HttpDelete("id")]
        public async Task<string> DeleteAsync(int id)
        {
            return await _api.DeletePersonAsync(id);
        }
    }
}
