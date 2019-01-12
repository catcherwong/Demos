namespace WintonExtConsulDemo
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading;
    using Winton.Extensions.Configuration.Consul;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, builder) =>
                    {
                        builder
                            .AddConsul(
                                "App1/appsettings.json",
                                cancellationTokenSource.Token,
                                options =>
                                {
                                    options.ConsulConfigurationOptions =
                                        cco => { cco.Address = new Uri("http://127.0.0.1:8500"); };
                                    options.Optional = true;
                                    options.ReloadOnChange = true;
                                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                                })
                            .AddEnvironmentVariables();
                    })
                .UseStartup<Startup>()
                .Build()
                .Run();

            cancellationTokenSource.Cancel();
        }    
    }
}
