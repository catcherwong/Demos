using System;
using Nancy.Hosting.Self;

namespace SelfHostingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var nancySelfHost = new NancyHost(new Uri("http://localhost:8888/")))
            {
                nancySelfHost.Start();
                Console.WriteLine("NancySelfHost已启动。。");
                try
                {
                    Console.WriteLine("正在启动 http://localhost:8888/ ");
                    System.Diagnostics.Process.Start("http://localhost:8888/");
                    Console.WriteLine("成功启动 http://localhost:8888/ ");
                }
                catch (Exception)
                {
                }
                Console.Read();
            }
            Console.WriteLine("http://localhost:8888 已经停止 \n NancySelfHost已关闭。。");            
        }
    }
}