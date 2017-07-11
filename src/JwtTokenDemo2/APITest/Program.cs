using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace APITest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient _client = new HttpClient();
            
            _client.DefaultRequestHeaders.Clear();

            //ExpireIn(_client);
            Refresh(_client);
            
            Console.Read();
        }

        private static void Refresh(HttpClient _client)
        {
            var client_id = "100";
            var client_secret = "888";
            var username = "Member";
            var password = "123";
            
            var asUrl = $"http://localhost:5001/api/token/auth?grant_type=password&client_id={client_id}&client_secret={client_secret}&username={username}&password={password}";

            Console.WriteLine("begin authorizing:");

            HttpResponseMessage asMsg = _client.GetAsync(asUrl).Result;
            
            string result = asMsg.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<ResponseData>(result);

            if (responseData.Code != "999")
            {
                Console.WriteLine("authorizing fail");
                return;
            }

            var token = JsonConvert.DeserializeObject<Token>(responseData.Data);

            Console.WriteLine("authorizing successfully");            
            Console.WriteLine($"the response of authorizing {result}");            

            Console.WriteLine("sleep 2min to make the token expire!!!");
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(2));

            Console.WriteLine("begin to request the resouce server");

            var rsUrl = "http://localhost:5002/api/values/1";
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);
            HttpResponseMessage rsMsg = _client.GetAsync(rsUrl).Result;


            Console.WriteLine("result of requesting the resouce server");
            Console.WriteLine(rsMsg.StatusCode);
            Console.WriteLine(rsMsg.Content.ReadAsStringAsync().Result);

            //refresh the token
            if (rsMsg.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("begin to refresh token");

                var refresh_token = token.refresh_token;
                asUrl = $"http://localhost:5001/api/token/auth?grant_type=refresh_token&client_id={client_id}&client_secret={client_secret}&refresh_token={refresh_token}";
                HttpResponseMessage asMsgNew = _client.GetAsync(asUrl).Result;
                string resultNew = asMsgNew.Content.ReadAsStringAsync().Result;

                var responseDataNew = JsonConvert.DeserializeObject<ResponseData>(resultNew);

                if (responseDataNew.Code != "999")
                {
                    Console.WriteLine("refresh token fail");
                    return;
                }

                Token tokenNew = JsonConvert.DeserializeObject<Token>(responseDataNew.Data);

                Console.WriteLine("refresh token successful");
                Console.WriteLine(asMsg.StatusCode);
                Console.WriteLine($"the response of refresh token {resultNew}");

                Console.WriteLine("requset resource server again");

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenNew.access_token);
                HttpResponseMessage rsMsgNew = _client.GetAsync("http://localhost:5002/api/values/1").Result;


                Console.WriteLine("the response of resource server");
                Console.WriteLine(rsMsgNew.StatusCode);
                Console.WriteLine(rsMsgNew.Content.ReadAsStringAsync().Result);
            }
        }

        private static void ExpireIn(HttpClient _client)
        {
            var client_id = "100";
            var client_secret = "888";
            var username = "Member";
            var password = "123";

            var asUrl = $"http://localhost:5001/api/token/auth?grant_type=password&client_id={client_id}&client_secret={client_secret}&username={username}&password={password}";

            Console.WriteLine("begin authorizing:");

            HttpResponseMessage asMsg = _client.GetAsync(asUrl).Result;
            
            string result = asMsg.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<ResponseData>(result);

            var token = JsonConvert.DeserializeObject<Token>(responseData.Data);

            Console.WriteLine("authorizing successfully");
            Console.WriteLine(asMsg.StatusCode);
            Console.WriteLine(result);

            Console.WriteLine("sleep 2min to make the token expire!!!");
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(2));

            Console.WriteLine("begin to request the resouce server");

            var rsUrl = "http://localhost:5002/api/values/1";

            HttpResponseMessage rsMsg = _client.GetAsync(rsUrl).Result;


            Console.WriteLine("the response of resource server");
            Console.WriteLine(rsMsg.StatusCode);
            Console.WriteLine(rsMsg.Content.ReadAsStringAsync().Result);
        }
    }

    public class Token
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public double expires_in { get; set; }
    }

    public class ResponseData
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}
