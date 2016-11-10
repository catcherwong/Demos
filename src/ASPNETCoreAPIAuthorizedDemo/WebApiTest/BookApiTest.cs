using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebApiTest
{
    public class BookApiTest
    {
        private HttpClient _client;
        private string applicationId = "1";
        private string applicationPassword = "123";
        private string timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
        private string nonce = new Random().Next(1000, 9999).ToString();
        private string signature = string.Empty;

        public BookApiTest()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8091/");
            _client.DefaultRequestHeaders.Clear();
            signature = HMACMD5Helper.GetEncryptResult($"{applicationId}-{timestamp}-{nonce}", "@*api#%^@");
        }

        [Fact]
        public async Task book_api_get_by_id_should_success()
        {
            string queryString = $"applicationId={applicationId}&timestamp={timestamp}&nonce={nonce}&signature={signature}&applicationPassword={applicationPassword}";
            
            HttpResponseMessage message = await _client.GetAsync($"api/book/4939?{queryString}");
            var result = JsonConvert.DeserializeObject<CommonResult<Book>>(message.Content.ReadAsStringAsync().Result);

            Assert.Equal("000", result.Code);
            Assert.Equal(4939, result.Data.Id);
            Assert.True(message.IsSuccessStatusCode);
        }

        [Fact]
        public async Task book_api_get_by_id_should_failure()
        {
            string inValidSignature = Guid.NewGuid().ToString();
            string queryString = $"applicationId={applicationId}&timestamp={timestamp}&nonce={nonce}&signature={inValidSignature}&applicationPassword={applicationPassword}";

            HttpResponseMessage message = await _client.GetAsync($"api/book/4939?{queryString}");
            var result = JsonConvert.DeserializeObject<CommonResult<Book>>(message.Content.ReadAsStringAsync().Result);

            Assert.Equal("401", result.Code);
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, message.StatusCode);            
        }

        [Fact]
        public async Task book_api_post_by_id_should_success()
        {              
            var data = new Dictionary<string, string>();
            data.Add("applicationId", applicationId);
            data.Add("applicationPassword", applicationPassword);
            data.Add("timestamp", timestamp);
            data.Add("nonce", nonce);
            data.Add("signature", signature);
            data.Add("Id", "4939");
            HttpContent ct = new FormUrlEncodedContent(data);

            HttpResponseMessage message = await _client.PostAsync("api/book", ct);
            var result = JsonConvert.DeserializeObject<CommonResult<Book>>(message.Content.ReadAsStringAsync().Result);

            Assert.Equal("000", result.Code);
            Assert.Equal(4939, result.Data.Id);
            Assert.True(message.IsSuccessStatusCode);

        }

        [Fact]
        public async Task book_api_post_by_id_should_failure()
        {
            string inValidSignature = Guid.NewGuid().ToString();
            var data = new Dictionary<string, string>();
            data.Add("applicationId", applicationId);
            data.Add("applicationPassword", applicationPassword);
            data.Add("timestamp", timestamp);
            data.Add("nonce", nonce);
            data.Add("signature", inValidSignature);
            data.Add("Id", "4939");
            HttpContent ct = new FormUrlEncodedContent(data);

            HttpResponseMessage message = await _client.PostAsync("api/book", ct);
            var result = JsonConvert.DeserializeObject<CommonResult<Book>>(message.Content.ReadAsStringAsync().Result);

            Assert.Equal("401", result.Code);
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, message.StatusCode);
        }
    }   
}
