using JWT.Common.Helpers;
using JWT.Common.Middlewares.TokenProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthorizedServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRedis(options =>
            //{
            //    options.MasterServer = Configuration.GetSection("RedisConfig")["MasterServer"];
            //    options.SlaveServer = Configuration.GetSection("RedisConfig")["SlaveServer"];
            //});

            //services.AddSingleton<RedisHelper>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            app.UseTokenProvider(new TokenProviderOptions
            {
                Audience = "Catcher Wong",
                Issuer = "http://catcher1994.cnblogs.com/",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            });

            app.UseMvc();
        }
    }
}
