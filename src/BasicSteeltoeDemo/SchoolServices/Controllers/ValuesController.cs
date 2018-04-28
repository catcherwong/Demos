namespace SchoolServices.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SchoolServices.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IStudentService _services;

        public ValuesController(IStudentService service)
        {
            this._services = service;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "school name";
        }

        // GET api/values/students
        [HttpGet]
        [Route("students")]
        public async Task<string> GetStudents(string name)
        {
            return await _services.GetStudentListAsync(name);
        }

        // GET: api/values/studentshy
        [HttpGet]
        [Route("studentshy")]
        public async Task<string> GetStudentsHystrix(
            [FromServices] StudentServiceHystrixCommand command
            , string name)
        {
            return await command.GetStudentListAsync(name);
        }
    }
}
