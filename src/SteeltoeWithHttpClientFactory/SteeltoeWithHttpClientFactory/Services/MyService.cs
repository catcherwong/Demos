using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteeltoeWithHttpClientFactory.Services
{
    public class MyService : IMyService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private const string MY_URL = "";

        public MyService(HttpClient httpClient, ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<MyService>();
            _httpClient = httpClient;
        }

        public async Task<string> GetTextAsync()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, MY_URL);
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            var result = await responseMessage.Content.ReadAsStringAsync();

            _logger.LogInformation("GetTextAsync: {0}", result);
            return result;
        }
    }

    public interface IMyService
    {
        Task<string> GetTextAsync();
    }
}
