namespace CBDemo
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:9000")
                .ConfigureLogging(logging =>
                {                    
                    logging.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Critical);
                })
                .Build();
    }
}
