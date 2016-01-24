using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TopShelfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                    //1
            {
                x.Service<NancySelfHost>(s =>                       //2
                {
                    s.ConstructUsing(name => new NancySelfHost());  //3
                    s.WhenStarted(tc => tc.Start());                //4
                    s.WhenStopped(tc => tc.Stop());                 //5
                });
                x.RunAsLocalSystem();                               //6
                x.SetDescription("Sample Topshelf Host");           //7
                x.SetDisplayName("Catcher Wong");                   //8
                x.SetServiceName("Nancy的Host");                    //9
            });
        }
    }
}
