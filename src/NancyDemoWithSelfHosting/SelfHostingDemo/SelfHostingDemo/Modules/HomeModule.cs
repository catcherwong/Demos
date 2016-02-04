using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
namespace SelfHostingDemo.Modules
{
    public class HomeModule:NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["index"];
            }; 

        }
    }
}
