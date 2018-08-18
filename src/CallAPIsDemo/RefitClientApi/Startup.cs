namespace RefitClientApi
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Refit;
    using Steeltoe.Common.Http.Discovery;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRefitClient<IPersonsApi>()
                    .ConfigureHttpClient(options =>
                    {
                        options.BaseAddress = new Uri(Configuration.GetValue<string>("personapi_url"));
                        //other settings of httpclient
                    })
                    //Steeltoe discovery
                    //.AddHttpMessageHandler<DiscoveryHttpMessageHandler>()
                    ;

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
