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
            HostFactory.Run(x =>                                    
            {
                x.Service<NancySelfHost>(s =>                       
                {
                    s.ConstructUsing(name => new NancySelfHost());  
                    s.WhenStarted(tc => tc.Start());                
                    s.WhenStopped(tc => tc.Stop());                 
                });
                x.RunAsLocalSystem();                                               
                x.SetDescription("Sample Topshelf Host");           
                x.SetDisplayName("Catcher Wong");                   
                x.SetServiceName("Nancy的Host");                    
            });
        }
    }
}
