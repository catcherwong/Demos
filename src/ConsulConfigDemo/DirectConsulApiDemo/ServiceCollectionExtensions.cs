namespace DirectConsulApiDemo
{
    using Consul;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration["Consul:Host"];
                consulConfig.Address = new Uri(address);
                //consulConfig.Address = new Uri("http://127.0.0.1:8500");

            }, null, handlerOverride =>
            {
                handlerOverride.Proxy = null;
                handlerOverride.UseProxy = false;
            }));
            return services;
        }
    }
}
