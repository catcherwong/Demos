using System;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extensions.DependencyInjection;

namespace NamedDiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //new AutofacDI().Handle();
            new MsDI().Handle();
            Console.ReadKey();
        }
    }

    class MsDI
    {
        public void Handle()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IInter,MyInter>(x => 
            {
                return new MyInter("1");
            });

            services.AddSingleton<IInter, MyInter>(x =>
            {
                return new MyInter("2");
            });

            var provider = services.BuildServiceProvider();

            var iInters = provider.GetService<IEnumerable<IInter>>();

            Console.WriteLine(iInters.Count());
        }
    }

    class AutofacDI
    {
        public void Handle()
        {
            var cb = new ContainerBuilder();
            cb.Register(c => new MyInter("1")).Named<IInter>("impl1");
            cb.Register(c => new MyInter("2")).Named<IInter>("impl2");
            var container = cb.Build();

            var first =  container.ResolveNamed<IInter>("impl1");
            var second = container.ResolveNamed<IInter>("impl2");

            Console.WriteLine(first.Say("catcher"));
            Console.WriteLine(second.Say("catcher"));
        }
    }


    public interface IInter
    {
        string Say(string name);
    }

    public class MyInter : IInter
    {
        private readonly string _id;
        public MyInter(string id)
        {
            this._id = id;
        }
        public string Say(string name)
        {
            return $"{_id}-hello,{name}";
        }
    }
}
