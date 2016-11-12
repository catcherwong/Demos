using JWT.Common;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace APITest
{
    public class ValueAPITest
    {

        private HttpClient _client;
        public ValueAPITest()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
        }

        [Fact]
        public void value_api_should_return_unauthorized_without_auth()
        {           
            //act         
            HttpResponseMessage message = _client.GetAsync("http://localhost:63324/api/values/1").Result;
            string result = message.Content.ReadAsStringAsync().Result;
         
            //assert
            Assert.False(message.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Unauthorized,message.StatusCode);
            Assert.Empty(result);
        }

        [Fact]
        public void value_api_should_return_result_without_authorize_attribute()
        {
            //act         
            HttpResponseMessage message = _client.GetAsync("http://localhost:63324/api/values").Result;
            string result = message.Content.ReadAsStringAsync().Result;
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(result);

            //assert
            Assert.True(message.IsSuccessStatusCode);
            Assert.Equal(2, res.Length);
        }

        [Fact]
        public void value_api_should_success_by_valid_auth()
        {
            //arrange
            var data = new Dictionary<string, string>();
            data.Add("username", "Member");
            data.Add("password", "123");
            HttpContent ct = new FormUrlEncodedContent(data);

            //act
            var obj = GetAccessToken(ct);                        
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + obj.access_token);
            HttpResponseMessage message = _client.GetAsync("http://localhost:63324/api/values/1").Result;
            string result = message.Content.ReadAsStringAsync().Result;

            //assert
            Assert.True(message.IsSuccessStatusCode);
            Assert.Equal(3, obj.access_token.Split('.').Length);
            Assert.Equal("value",result);            
        }


        private Token GetAccessToken(HttpContent content)
        {
            HttpResponseMessage message_token = _client.PostAsync("http://127.0.0.1:8000/auth/token", content).Result;
            string res = message_token.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(res);
        }

    }
}
