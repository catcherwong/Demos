namespace APIGateway.Controllers
{
    using APIGateway.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(
            IStudentService studentService
            )
        {
            this._studentService = studentService;
        }

        // GET api/students/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            return await _studentService.GetStudentName(id);
        }       
    }
}
