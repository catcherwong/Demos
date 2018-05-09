using System;
using Polly;
using Polly.Wrap;

namespace PollyCBDemo
{
    class Program
    {
        private static PolicyWrap<string> policy;

        public Program()
        {
            Action<Exception, TimeSpan> onBreak = (exception, timespan) => { Console.WriteLine("onbreak"); };
            Action onReset = () => { Console.WriteLine("onreset"); };
            var breaker = Policy
                .Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(1), onBreak, onReset);
            
            policy =
                Policy<string>
                    .Handle<Exception>()
                    .Fallback("fallback",(x)=>
                    {
                        Console.WriteLine("执行失败，返回Fallback");
                    })
                    .Wrap(breaker);
   
        }

        static void Main(string[] args)
        {            
            int i = 0;

            while(i<15)
            {
                var res = policy.Execute(() =>
                {
                    throw new Exception("test");
                });
                i++;
            }

            Console.WriteLine("Hello World!");
            Console.Read();
        }

    }
}
