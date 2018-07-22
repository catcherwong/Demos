using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Fallback;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine($"BasicUsage, StatusCode = {BasicUsage().GetAwaiter().GetResult()}");

            //Console.WriteLine($"NamedUsage, StatusCode = {NamedUsage().GetAwaiter().GetResult()}");

            //Console.WriteLine($"TypedUsage, StatusCode = {TypedUsage().GetAwaiter().GetResult()}");

            //Console.WriteLine($"DelegatingHandlerUsage, StatusCode = {DelegatingHandlerUsage().GetAwaiter().GetResult()}");

            Console.WriteLine($"BasicPollyUsage, StatusCode = {BasicPollyUsage().GetAwaiter().GetResult()}");

            Console.WriteLine($"TimeOutUsage, StatusCode = {TimeOutUsage().GetAwaiter().GetResult()}");





            Console.WriteLine("Hello World!");
            Console.Read();
        }

        static async Task<string> BasicUsage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
            var services = serviceCollection.BuildServiceProvider();
            var clientFactory = services.GetService<IHttpClientFactory>();

            var client = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.github.com");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            return response.StatusCode.ToString();
        }

        static async Task<string> NamedUsage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient("basic", x =>
             {
                 x.BaseAddress = new Uri("https://www.github.com");
                 x.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
             });
            var services = serviceCollection.BuildServiceProvider();
            var clientFactory = services.GetService<IHttpClientFactory>();

            var client = clientFactory.CreateClient("basic");
            var request = new HttpRequestMessage(HttpMethod.Get, "/");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            return response.StatusCode.ToString();
        }

        #region Type Usage
        private class MyTypeService
        {
            private readonly HttpClient _client;

            public MyTypeService(HttpClient client)
            {
                this._client = client;
            }

            public async Task<string> GetResponse()
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/");

                var response = await _client.SendAsync(request).ConfigureAwait(false);

                return response.StatusCode.ToString();
            }
        }

        static async Task<string> TypedUsage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient<MyTypeService>(x =>
            {
                x.BaseAddress = new Uri("https://www.github.com");
                x.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
            });
            var services = serviceCollection.BuildServiceProvider();
            var service = services.GetService<MyTypeService>();

            return await service.GetResponse().ConfigureAwait(false);
        }
        #endregion

        #region Outgoing request middleware   (DelegatingHandler)
        private class ValidateHeaderHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                if (!request.Headers.Contains("X-API-KEY"))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("You must supply an API key header called X-API-KEY")
                    };
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }

        static async Task<string> DelegatingHandlerUsage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ValidateHeaderHandler>();
            serviceCollection.AddHttpClient("handler", x =>
            {
                x.BaseAddress = new Uri("https://www.github.com");
                x.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
            }).AddHttpMessageHandler<ValidateHeaderHandler>();
            var services = serviceCollection.BuildServiceProvider();
            var clientFactory = services.GetService<IHttpClientFactory>();

            var client = clientFactory.CreateClient("handler");
            var request = new HttpRequestMessage(HttpMethod.Get, "/");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            return response.StatusCode.ToString();
        }
        #endregion



        #region Polly Usage
        static async Task<string> BasicPollyUsage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient("basicPolly", x =>
            {
                x.BaseAddress = new Uri("http://localhost:8000");
                x.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");

            }).AddTransientHttpErrorPolicy(p => p.RetryAsync(3, (e, i) =>
               {
                   Console.WriteLine($"retry - {i}");
               }));
            var services = serviceCollection.BuildServiceProvider();
            var clientFactory = services.GetService<IHttpClientFactory>();

            var client = clientFactory.CreateClient("basicPolly");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/values");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            return response.StatusCode.ToString();
        }


        static async Task<string> TimeOutUsage()
        {
            var fallbackResponse = new HttpResponseMessage();
            fallbackResponse.Content = new StringContent("fallback");
            fallbackResponse.StatusCode = HttpStatusCode.TooManyRequests;



            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient("timeout", x =>
            {
                x.BaseAddress = new Uri("http://localhost:8000");
                x.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
            })
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().FallbackAsync(fallbackResponse))
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(1), (ex, ts) =>
            {
                Console.WriteLine($" break.. {ts.TotalMilliseconds}");
            }, () =>
            {
                Console.WriteLine("reset");
            }))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));



            var services = serviceCollection.BuildServiceProvider();
            var clientFactory = services.GetService<IHttpClientFactory>();

            var client = clientFactory.CreateClient("timeout");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/values/timeout");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            Console.WriteLine(response.StatusCode.ToString());
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);


            return response.StatusCode.ToString();
        }


        #endregion


    }
}
