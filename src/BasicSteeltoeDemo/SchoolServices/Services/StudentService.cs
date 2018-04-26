namespace SchoolServices.Services
{ 
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Steeltoe.Common.Discovery;

    public class StudentService : IStudentService
    {
        DiscoveryHttpClientHandler _handler;
        ILogger<StudentService> _logger;
        private const string GET_STUDENT_URL = "http://studentservice/api/values/school";

        public StudentService(IDiscoveryClient client, ILoggerFactory logFactory)
        {
            _handler = new DiscoveryHttpClientHandler(client, logFactory.CreateLogger<DiscoveryHttpClientHandler>());
            _logger = logFactory.CreateLogger<StudentService>();
        }

        public async Task<string> GetStudentListAsync(string name)
        {
            var client = GetClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, GET_STUDENT_URL);

            httpRequestMessage.Content = new StringContent(name, System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(httpRequestMessage);

            var result = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("GetStudentListAsync: {0}", result);
            return result;
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient(_handler, false);
            return client;
        }
    }
}
