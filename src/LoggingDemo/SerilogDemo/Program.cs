namespace SerilogDemo
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;    
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                   .UseSerilog((hostingContext, loggerConfiguration) => 
                               loggerConfiguration.WriteTo.File("demo.log",
                                                                rollingInterval: RollingInterval.Day
                                                               ))
                .Build();
    }
}
