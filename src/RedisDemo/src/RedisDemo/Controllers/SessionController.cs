using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisDemo.Common;

namespace RedisDemo.Controllers
{
    public class SessionController : Controller
    {
        //private IRedis _redis;
        private ISession _session;
        public SessionController(ISession session)
        {
            _session = session;
        }

        [HttpGet("/")]
        [ResponseCache(NoStore = true)]
        public IActionResult Index()
        {
            ViewBag.Site = "site 1";
            return View();
        }
        [HttpPost("/")]
        public IActionResult Index(string sessionName, string sessionValue)
        {
            //set the session
            _session.SetExtension(sessionName, sessionValue);
            //HttpContext.Session.Set(sessionName,System.Text.Encoding.UTF8.GetBytes(sessionValue));
            return Redirect("/about?sessionName=" + sessionName);
        }

        [HttpGet("/about")]
        [ResponseCache(NoStore = true)]
        public IActionResult About(string sessionName)
        {
            //get the session
            ViewBag.Session = _session.GetExtension(sessionName);

            ViewBag.Site = "site 1";
            //byte[] bytes;
            //if (HttpContext.Session.TryGetValue(sessionName, out bytes))
            //{
            //    ViewBag.Session = System.Text.Encoding.UTF8.GetString(bytes);
            //}
            //else
            //{
            //    ViewBag.Session = "empty";
            //}
            return View();
        }
    }
}
