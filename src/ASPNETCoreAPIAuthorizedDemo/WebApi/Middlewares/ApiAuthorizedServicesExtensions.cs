using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApi.Middlewares
{
    public static class ApiAuthorizedServicesExtensions
    {

        /// <summary>
        /// Add response compression services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns></returns>
        public static IServiceCollection AddApiAuthorized(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services;
        }

        /// <summary>
        /// Add response compression services and configure the related options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <param name="configureOptions">A delegate to configure the <see cref="ResponseCompressionOptions"/>.</param>
        /// <returns></returns>
        public static IServiceCollection AddApiAuthorized(this IServiceCollection services, Action<ApiAuthorizedOptions> configureOptions)
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
