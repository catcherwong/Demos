using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OwinSelfDemo.Startup))]

namespace OwinSelfDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            app.UseNancy();
        }
    }
}
