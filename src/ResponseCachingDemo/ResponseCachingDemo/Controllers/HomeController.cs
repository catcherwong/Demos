namespace ResponseCachingDemo.Controllers
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    //直接一，简单粗暴，不要拼写错了就好~~
        //    Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] = "public, max-age=600";

        //    ////直接二，略微优雅点
        //    //Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        //    //{
        //    //    Public = true,
        //    //    MaxAge = TimeSpan.FromSeconds(20)
        //    //};

        //    return View();
        //}

        //1. public, max-age=600
        [ResponseCache(Duration = 600)]
        //2. private,max-age=600
        //[ResponseCache(Duration = 600, Location = ResponseCacheLocation.Client)]
        //3. no-store,no-cache
        //[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        //4. with vary
        //[ResponseCache(Duration = 600, VaryByHeader = "User-Agent")]
        //5.
        //[ResponseCache(CacheProfileName = "default")]
        public IActionResult Index()
        {         
            return View();
        }            
    }
}
