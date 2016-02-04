using Nancy.Hosting.Self;
using System;

namespace TopShelfDemo
{
    public class NancySelfHost
    {
        private NancyHost _nancyHost;

        public void Start()
        {
            const string uriStr = "http://localhost:8888/topshelf-nancy/";
            _nancyHost = new NancyHost(new Uri(uriStr));
            _nancyHost.Start();
            try
            {
                System.Diagnostics.Process.Start(uriStr);
            }
            catch (Exception)
            {                
            }

            Console.WriteLine("监听ing - " + uriStr);
        }

        public void Stop()
        {
            _nancyHost.Stop();
            Console.WriteLine("下次再见！");
        }
    }
}