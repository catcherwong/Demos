namespace AspNetCoreDemo.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using FluentValidation;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IStudentService _service;
        public ValuesController(IStudentService service)
        {
            this._service = service;
        }

        // GET api/values/hobbies1
        [HttpGet("hobbies1")]
        public ActionResult GetHobbies1([FromQuery]QueryStudentHobbiesDto dto)
        {
            var validator = new QueryStudentHobbiesDtoValidator();
            var results = validator.Validate(dto, ruleSet: "all");

            return !results.IsValid
                       ? Ok(new { code = -1, data = new List<string>(), msg = results.Errors.FirstOrDefault().ErrorMessage })
                       : Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }

        // GET api/values/hobbies2
        [HttpGet("hobbies2")]
        public ActionResult GetHobbies2([FromQuery][CustomizeValidator(RuleSet = "all")]QueryStudentHobbiesDto dto)
        {       
            //isn't valid will not visit the okobjectresult
            return Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }

        // GET api/values/hobbies3
        [HttpGet("hobbies3")]
        [ValidateFilterAttribute]
        public ActionResult GetHobbies3([FromQuery][CustomizeValidator(RuleSet = "all")]QueryStudentHobbiesDto dto)
        {
            //isn't valid will not visit the okobjectresult, but visit the filter
            return Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }

        // GET api/values/hobbies4
        [HttpGet("hobbies4")]
        public ActionResult GetHobbies4([FromQuery]int? id,[FromQuery]string name)
        {
            var (flag, msg) = _service.QueryHobbies(new QueryStudentHobbiesDto 
            {
                Id =id,
                Name = name
            });

            return !flag
                ? Ok(new { code = -1, data = new List<string>(), msg = msg })
                : Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }

    }
}
