namespace WebApiClientApi
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using WebApiClient;

    /// <summary>
    /// WebApiClient extension.
    /// </summary>
    public static class WebApiClientExtension
    {
        /// <summary>
        /// Adds the http API client.
        /// </summary>
        /// <returns>The http API client.</returns>
        /// <param name="services">Services.</param>
        /// <param name="config">Config.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IHttpClientBuilder AddHttpApiClient<TInterface>(this IServiceCollection services, Action<HttpApiConfig> config = null) where TInterface : class, IHttpApi
        {
            return services.AddHttpClient(typeof(TInterface).Name).AddTypedClient<TInterface>((client, provider) =>
            {
                var httpApiConfig = new HttpApiConfig(client);
                config?.Invoke(httpApiConfig);
                return HttpApiClient.Create<TInterface>(httpApiConfig);
            });
        }
    }
}
