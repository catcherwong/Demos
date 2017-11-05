namespace CachingWithAspectCore
{
    using AspectCore.Configuration;
    using AspectCore.Extensions.DependencyInjection;
    using AspectCore.Injector;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Reflection;
    using CachingWithAspectCore.QCaching;
    using CachingWithAspectCore.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<ICachingProvider, MemoryCachingProvider>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            //handle BLL class
            var assembly = this.GetType().GetTypeInfo().Assembly;
            this.AddBLLClassToServices(assembly, services);

            var container = services.ToServiceContainer();
            container.AddType<QCachingInterceptor>();
            container.Configure(config =>
            {
                config.Interceptors.AddTyped<QCachingInterceptor>(method => typeof(IQCaching).IsAssignableFrom(method.DeclaringType));
            });

            return container.Build();
        }

        public void AddBLLClassToServices(Assembly assembly, IServiceCollection services)
        {
            var types = assembly.GetTypes().ToList();

            foreach (var item in types.Where(x => x.Name.EndsWith("BLL", StringComparison.OrdinalIgnoreCase) && x.IsClass))
            {
                services.AddSingleton(item);
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
