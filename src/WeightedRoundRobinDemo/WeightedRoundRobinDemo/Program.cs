namespace WeightedRoundRobinDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    class Program
    {
        static void Main(string[] args)
        {
            var lbUrls = new Dictionary<string,int>
            {
                {"http://10.0.10.1/api/values",5},
                {"http://10.0.10.2/api/values",1},
                {"http://10.0.10.3/api/values",1},
            };

            var robin = new WeightedRoundRobin<string>(lbUrls);

            var visitCount = lbUrls.Values.Sum() * new Random().Next(3, 5);

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
