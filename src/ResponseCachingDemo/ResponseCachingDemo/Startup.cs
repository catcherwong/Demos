namespace ResponseCachingDemo
{    
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
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
            services.AddResponseCaching();

            services.AddMvc(options=>
            {
                options.CacheProfiles.Add("default", new Microsoft.AspNetCore.Mvc.CacheProfile 
                {
                    Duration = 600,  // 10 min
                });
                
                options.CacheProfiles.Add("Hourly", new Microsoft.AspNetCore.Mvc.CacheProfile
                {
                    Duration = 60 * 60,  // 1 hour
                    //Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Any,
                    //NoStore = true,
                    //VaryByHeader = "User-Agent",
                    //VaryByQueryKeys = new string[] { "aaa" }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCaching();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                    { 
                        Public = true,
                        //for 1 year
                        MaxAge = System.TimeSpan.FromDays(365)
                    };
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
