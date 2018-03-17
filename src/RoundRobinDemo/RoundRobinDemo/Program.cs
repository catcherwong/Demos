namespace RoundRobinDemo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var lbUrls = new List<string>
            {
                "http://10.0.10.1/api/values",
                "http://10.0.10.2/api/values",
                "http://10.0.10.3/api/values",
                "http://10.0.10.4/api/values",
            };

            var robin = new RoundRobin<string>(lbUrls);

            var visitCount = lbUrls.Count * new Random().Next(3, 5);

            Console.WriteLine("begin one by one..");
            for (int i = 0; i < visitCount; i++)
            {
                Console.WriteLine($"{i + 1}:Sending request to {robin.GetNextItem()}");
            }

            Console.WriteLine("begin parallel..");
            Parallel.For(0, visitCount, i =>
            {
                Console.WriteLine($"{i + 1}:Sending request to {robin.GetNextItem()}");
            });

            Console.ReadKey();
        }
    }
}
