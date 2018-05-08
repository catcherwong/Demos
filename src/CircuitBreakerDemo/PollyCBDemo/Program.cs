using System;
using Polly;

namespace PollyCBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var fallBackPolicy =
                Policy
                    .Handle<Exception>()
                    .Fallback(()=>Console.WriteLine( "执行失败，返回Fallback"));

            Action<Exception, TimeSpan> onBreak = (exception, timespan) => { Console.WriteLine("onbreak"); };
            Action onReset = () => { Console.WriteLine("onreset"); };
            var breaker = Policy
                .Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(1), onBreak, onReset);

            var retry = Policy
                .Handle<Exception>()
                  .Retry(4);

            Policy.Wrap(breaker,fallBackPolicy, retry).Execute(()=>
            {
                Console.WriteLine("111");
                using(System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1);
                    var res = client.GetAsync("http://yourdomain.com").Result;
                    Console.WriteLine(res.IsSuccessStatusCode);
                }
            });




            Console.WriteLine("Hello World!");
            Console.Read();
        }

    }
}
