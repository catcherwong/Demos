namespace OrderApi
{
    using System.Reflection;
    using Jaeger;
    using Jaeger.Samplers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using OpenTracing;
    using OpenTracing.Util;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<OrderDbContext>(options =>
                {
                    var connectionStringBuilder = new SqliteConnectionStringBuilder
                    {
                        DataSource = ":memory:",
                        Mode = SqliteOpenMode.Memory,
                        Cache = SqliteCacheMode.Shared
                    };
                    var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                    // Hack: EFCore resets the DB for every connection so we keep the connection open.
                    // This is obviously just demo code :)
                    connection.Open();
                    connection.EnableExtensions(true);

                    options.UseSqlite(connection);
                });

            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = Assembly.GetEntryAssembly().GetName().Name;

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddOpenTracing();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
                dbContext.Seed();
            }

            app.UseMvc();
        }
    }
}
