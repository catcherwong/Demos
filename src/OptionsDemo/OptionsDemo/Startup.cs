namespace OptionsDemo
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DemoOptions>(Configuration.GetSection("Demo"));
            services.Configure<DemoOptions>("Sec",Configuration.GetSection("DemoWithName"));

            //services.PostConfigure<DemoOptions>(x => 
            //{
            //    x.Age = 100;
            //});

            services.PostConfigureAll<DemoOptions>(x =>
            {
                x.Age = 100;
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
