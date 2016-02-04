using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace OwinSelfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9000/";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("{0} 正在运行",url);
                System.Diagnostics.Process.Start(url);
                Console.Read();
            }
        }
    }
}
