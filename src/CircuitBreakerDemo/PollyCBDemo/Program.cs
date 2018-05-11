using System;
using Polly;
using Polly.Wrap;

namespace PollyCBDemo
{
    class Program
    {
        static PolicyWrap<string> policy = Policy<string>
                    .Handle<Exception>()
                    .Fallback("fallback", (x) =>
                    {
                        Console.WriteLine("执行失败，返回Fallback");
                    })
            .Wrap(
                Policy
                .Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(1),
                                onBreak: (exception, timespan) => { Console.WriteLine("onbreak"); },
                                onReset: () => { Console.WriteLine("onreset"); }))
            .Wrap(Policy
                  .Timeout(1,Polly.Timeout.TimeoutStrategy.Pessimistic,(context,ts,task)=>
                    {
                        Console.WriteLine("Timeout");
                    }));


        static void Main(string[] args)
        {            
            int i = 0;

            while(i<15)
            {
                var res = policy.Execute(() =>
                {
                    var rate = new Random().NextDouble();
                    Console.WriteLine($"rate = {rate}");
                    if(rate < 0.3)
                    {
                        throw new Exception("test");
                    }
                    else if(rate >= 0.3 && rate < 0.6)
                    {
                        System.Threading.Thread.Sleep(1001);
                        return "wong";
                    }
                    else
                    {
                        return "catcher";
                    }

                });
                Console.WriteLine(res);
                i++;
            }

            Console.WriteLine("Hello World!");
            Console.Read();
        }

    }
}
