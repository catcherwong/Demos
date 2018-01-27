namespace DIDemo
{
    using DIDemo.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IDemoService, DemoServiceA>();
            //services.AddSingleton<IDemoService, DemoServiceB>();

            services.AddSingleton<DemoServiceA>();
            services.AddSingleton<DemoServiceB>();

            services.AddSingleton(factory =>
            {
                Func<string, IDemoService> accesor = key =>
                {
                    if (key.Equals("ServiceA"))
                    {
                        return factory.GetService<DemoServiceA>();
                    }
                    else if (key.Equals("ServiceB"))
                    {
                        return factory.GetService<DemoServiceB>();
                    }
                    else
                    {
                        throw new ArgumentException($"Not Support key : {key}");
                    }
                };
                return accesor;
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
