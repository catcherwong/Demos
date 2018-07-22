namespace TestAPI
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Polly;
    using System.Net.Http;
    
    public class Startup
    {
        public Startup(IConfiguration configuration,ILoggerFactory factory)
        {
            Configuration = configuration;
            Logger = factory.CreateLogger<Startup>();
        }

        public IConfiguration Configuration { get; }

        public ILogger Logger { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var fallbackResponse = new HttpResponseMessage();
            fallbackResponse.Content = new StringContent("fallback");
            fallbackResponse.StatusCode = System.Net.HttpStatusCode.TooManyRequests;

            services.AddHttpClient("cb", x =>
            {
                x.BaseAddress = new Uri("http://localhost:8000");
                x.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
            })
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().FallbackAsync(fallbackResponse, async b =>
            {
                Logger.LogWarning($"fallback here {b.Exception.Message}");
            }))
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(4), (ex, ts) =>
            {

                Logger.LogWarning($"break here {ts.TotalMilliseconds}");
            }, () =>
            {                
                Logger.LogWarning($"reset here ");
            }))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
