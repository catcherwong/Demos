namespace APIGateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;    

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ISchoolService _schoolService;

        public ValuesController(
            ISchoolService schoolService
            )
        {
            this._schoolService = schoolService;
           
        }

        // GET api/values
        [HttpGet]
        [Route("")]
        public async Task<string> Get()
        {
            return await Task.FromResult("This is api gateway");
        }              

        // GET api/values/school
        [HttpGet]
        [Route("school")]
        public async Task<string> School()
        {
            return await _schoolService.GetName();
        }              
    }
}
