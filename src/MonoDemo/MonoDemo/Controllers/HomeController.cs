using Microsoft.AspNetCore.Mvc;

namespace MonoDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("running asp.net core 2.0 via mono!");
        }
    }
}
