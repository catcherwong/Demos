using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace OwinDemo.Modules
{
    public class HomeModule:NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "OwinDemo by Catcher Wong";
        }
    }
}