namespace TplDemo.Controllers
{    
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
    /// </summary>    
    public class HomeController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("~/swagger") ;
        }
    }
}
