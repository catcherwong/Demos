namespace APIGateway.Services
{
    using Microsoft.Extensions.Logging;
    using Steeltoe.Common.Discovery;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class StudentService : IStudentService
    {
        private const string GETNAME_URL = "http://studentservice/api/values";

        private readonly DiscoveryHttpClientHandler _handler;

        private readonly ILogger _logger;

        public StudentService(IDiscoveryClient client, ILogger logger)
        {
            _handler = new DiscoveryHttpClientHandler(client, logger);
            _logger = logger;
        }

        public async Task<string> GetStudentName(int id)
        {
            var client = GetClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{GETNAME_URL}/{id}");
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                _logger?.LogError("Invoke exception: {0}", e);
                throw;
            }
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient(_handler, false);
            return client;
        }
    }

    public interface IStudentService
    {
        Task<string> GetStudentName(int id);
    }
}
