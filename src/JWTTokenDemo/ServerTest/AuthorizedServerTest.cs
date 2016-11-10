using JWT.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ServerTest
{
    public class AuthorizedServerTest
    {
        private HttpClient _client;
        public AuthorizedServerTest()
        {
            _client = new HttpClient();            
            _client.DefaultRequestHeaders.Clear();
        }

        [Fact]
        public async Task authorized_server_should_generate_token_success()
        {
            //arrange
            var data = new Dictionary<string, string>();
            data.Add("username", "Member");
            data.Add("password", "123");
            HttpContent ct = new FormUrlEncodedContent(data);

            //act
            System.Net.Http.HttpResponseMessage message_token = await _client.PostAsync("http://127.0.0.1:8000/auth/token", ct);
            string res = await message_token.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(res);

            //assert
            Assert.NotNull(obj);
            Assert.Equal("600", obj.expires_in);
            Assert.Equal(3, obj.access_token.Split('.').Length);
            Assert.Equal("Bearer", obj.token_type);
        }

        [Fact]
        public async Task authorized_server_should_generate_token_fault_by_invalid_app()
        {
            //arrange
            var data = new Dictionary<string, string>();
            data.Add("username", "Member");
            data.Add("password", "123456");
            HttpContent ct = new FormUrlEncodedContent(data);

            //act
            System.Net.Http.HttpResponseMessage message_token = await _client.PostAsync("http://127.0.0.1:8000/auth/token", ct);
            var res = await message_token.Content.ReadAsStringAsync();
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(res);

            //assert
            Assert.Equal("invalid_grant", (string)obj.error);
            Assert.Equal(HttpStatusCode.BadRequest, message_token.StatusCode);
        }

        [Fact]
        public async Task authorized_server_should_generate_token_fault_by_invalid_httpmethod()
        {
            //arrange
            Uri uri = new Uri("http://127.0.0.1:8000/auth/token?username=Member&password=123456");

            //act
            System.Net.Http.HttpResponseMessage message_token = await _client.GetAsync(uri);
            var res = await message_token.Content.ReadAsStringAsync();
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(res);

            //assert
            Assert.Equal("invalid_grant", (string)obj.error);
            Assert.Equal(HttpStatusCode.BadRequest, message_token.StatusCode);
        }
    }
}
