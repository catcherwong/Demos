namespace ObjectPoolDemo
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.ObjectPool;

    class Program
    {
        private static int PoolSize = Environment.ProcessorCount * 2;

        static void Main(string[] args)
        {
            Demo item1 = null;
            Demo item2 = null;
            Demo item3 = null;


            //#1
            //Console.WriteLine("#1");
            //var defalutPolicy = new DefaultPooledObjectPolicy<Demo>();
            ////default maximumRetained is Environment.ProcessorCount * 2
            //var defaultPool = new DefaultObjectPool<Demo>(defalutPolicy);

            //for (int i = 0; i < PoolSize; i++)
            //{
            //    item1 = defaultPool.Get();
            //    Console.WriteLine($"#{i+1}-{item1.Id}-{item1.Name}-{item1.CreateTimte}");
            //}

            ////#2 
            //Console.WriteLine("#2");
            //var demoPolicy = new DemoPooledObjectPolicy();
            //var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy,1);

            //item1 = defaultPoolWithDemoPolicy.Get();
            //defaultPoolWithDemoPolicy.Return(item1);
            //item2 = defaultPoolWithDemoPolicy.Get();

            //Console.WriteLine($"{item1.Id}-{item1.Name}-{item1.CreateTimte}");
            //Console.WriteLine($"{item2.Id}-{item2.Name}-{item2.CreateTimte}");
            //Console.WriteLine(item1 == item2);

            //item3 = defaultPoolWithDemoPolicy.Get();
            //Console.WriteLine($"{item3.Id}-{item3.Name}-{item3.CreateTimte}");
            //Console.WriteLine(item3 == item1);


            ////#3
            //Console.WriteLine("#2");
            //var defaultProvider = new DefaultObjectPoolProvider();
            //var policy = new DemoPooledObjectPolicy();
            ////default maximumRetained is Environment.ProcessorCount * 2
            //ObjectPool<Demo> pool = defaultProvider.Create(policy);

            //item1 = pool.Get();
            //pool.Return(item1);
            //item2 = pool.Get();

            //Console.WriteLine($"{item1.Id}-{item1.Name}-{item1.CreateTimte}");
            //Console.WriteLine($"{item2.Id}-{item2.Name}-{item2.CreateTimte}");
            //Console.WriteLine(item1 == item2);

            //item3 = pool.Get();
            //Console.WriteLine($"{item3.Id}-{item3.Name}-{item3.CreateTimte}");
            //Console.WriteLine(item3 == item2);


            //#4
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton(s =>
            {
                var provider = s.GetRequiredService<ObjectPoolProvider>();
                return provider.Create(new DemoPooledObjectPolicy());
            });
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var pool = serviceProvider.GetService<ObjectPool<Demo>>();

            item1 = pool.Get();
            pool.Return(item1);
            item2 = pool.Get();

            Console.WriteLine($"{item1.Id}-{item1.Name}-{item1.CreateTimte}");
            Console.WriteLine($"{item2.Id}-{item2.Name}-{item2.CreateTimte}");
            Console.WriteLine(item1 == item2);

            item3 = pool.Get();
            Console.WriteLine($"{item3.Id}-{item3.Name}-{item3.CreateTimte}");
            Console.WriteLine(item3 == item2);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    public class DemoPooledObjectPolicy : IPooledObjectPolicy<Demo>
    {
        public Demo Create()
        {
            return new Demo { Id = 1, Name = "catcher", CreateTimte = DateTime.Now };
        }

        public bool Return(Demo obj)
        {
            return true;
        }
    }


    public class Demo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTimte { get; set; }
    }
}
