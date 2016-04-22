using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Catcher.AndroidDemo.Common
{
    public static class EasyWebRequest
    {
        /// <summary>
        /// send the post request based on HttpClient
        /// </summary>
        /// <param name="requestUrl">the url you post</param>
        /// <param name="routeParameters">the parameters you post</param>
        /// <returns>return a response object</returns>
        public static async Task<object> SendPostRequestBasedOnHttpClient(string requestUrl, IDictionary<string, string> routeParameters)
        {
            object returnValue = new object();
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            Uri uri = new Uri(requestUrl);
            var content = new FormUrlEncodedContent(routeParameters);
            try
            {
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var stringValue = await response.Content.ReadAsStringAsync();
                    returnValue = JsonObject.Parse(stringValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        /// <summary>
        /// send the get request based on HttpClient
        /// </summary>
        /// <param name="requestUrl">the url you post</param>
        /// <param name="routeParameters">the parameters you post</param>
        /// <returns>return a response object</returns>
        public static async Task<object> SendGetRequestBasedOnHttpClient(string requestUrl, IDictionary<string, string> routeParameters)
        {
            object returnValue = new object();
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //format the url paramters
            string paramters = string.Join("&", routeParameters.Select(p => p.Key + "=" + p.Value));
            Uri uri = new Uri(string.Format("{0}?{1}", requestUrl, paramters));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var stringValue = await response.Content.ReadAsStringAsync();
                    returnValue = JsonObject.Parse(stringValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }


        /// <summary>
        /// send the get request based on HttpWebRequest
        /// </summary>
        /// <param name="requestUrl">the url you post</param>
        /// <param name="routeParameters">the parameters you post</param>
        /// <returns>return a response object</returns>
        public static async Task<object> SendGetHttpRequestBaseOnHttpWebRequest(string requestUrl, IDictionary<string, string> routeParameters)
        {
            object returnValue = new object();
            string paramters = string.Join("&", routeParameters.Select(p => p.Key + "=" + p.Value));
            Uri uri = new Uri(string.Format("{0}?{1}", requestUrl, paramters));
            var request = (HttpWebRequest)HttpWebRequest.Create(uri);

            using (var response = request.GetResponseAsync().Result as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string stringValue = await reader.ReadToEndAsync();
                            returnValue = JsonObject.Parse(stringValue);
                        }
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// send the post request based on httpwebrequest
        /// </summary>
        /// <param name="url">the url you post</param>
        /// <param name="routeParameters">the parameters you post</param>
        /// <returns>return a response object</returns>
        public static async Task<object> SendPostHttpRequestBaseOnHttpWebRequest(string url, IDictionary<string, string> routeParameters)
        {
            object returnValue = new object();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";

            byte[] postBytes = null;
            request.ContentType = "application/x-www-form-urlencoded";
            string paramters = string.Join("&", routeParameters.Select(p => p.Key + "=" + p.Value));
            postBytes = Encoding.UTF8.GetBytes(paramters.ToString());

            using (Stream outstream = request.GetRequestStreamAsync().Result)
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }

            using (HttpWebResponse response = request.GetResponseAsync().Result as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string stringValue = await reader.ReadToEndAsync();
                            returnValue = JsonObject.Parse(stringValue);
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
