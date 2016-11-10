using Microsoft.Extensions.DependencyInjection;
using System;

namespace JWT.Common.Helpers
{
    public static class RedisService
    {
        /// <summary>
        /// Add response compression services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }
            services.Configure(configureOptions);
            return services;
        }

    }
}
