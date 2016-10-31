using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApi.CommandText;
using WebApi.Common;
using Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {

        private DapperHelper _helper;
        public BookController(DapperHelper helper)
        {
            this._helper = helper;
        }

        // GET: api/book
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _helper.QueryAsync(BookCommandText.GetBooks);
            CommonResult<Book> json = new CommonResult<Book>
            {
                Code = "000",
                Message = "ok",
                Data = res
            };
            return Ok(json);
        }

        // GET api/book/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            var res = _helper.Query<Book>(BookCommandText.GetBookById, dp, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            CommonResult<Book> json = new CommonResult<Book>
            {
                Code = "000",
                Message = "ok",
                Data = res
            };
            return Ok(json);
        }

        // POST api/book        
        [HttpPost]
        public IActionResult Post([FromForm]PostForm form)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", form.Id, DbType.Int32, ParameterDirection.Input);
            var res = _helper.Query<Book>(BookCommandText.GetBookById, dp, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            CommonResult<Book> json = new CommonResult<Book>
            {
                Code = "000",
                Message = "ok",
                Data = res
            };
            return Ok(json);
        }

    }

    public class PostForm
    {
        public string Id { get; set; }
    }

}
