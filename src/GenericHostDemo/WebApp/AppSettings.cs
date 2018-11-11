using System;
namespace WebApp
{
    public class AppSettings
    {
        public int PrinterDelaySecond { get; set; }
        public int TimerPeriod { get; set; }

        public string HostName { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
    }
}
