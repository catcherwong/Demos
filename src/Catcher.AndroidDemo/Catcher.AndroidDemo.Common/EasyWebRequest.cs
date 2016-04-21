using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Catcher.AndroidDemo.Common
{
    public static class EasyWebRequest
    {
        /// <summary>
        /// post the web request
        /// </summary>
        /// <param name="requestUrl">the url you post</param>
        /// <param name="routeParameters">parameters</param>
        /// <returns></returns>
        public static async Task<object> DoPostAndGetRetuanValue(string requestUrl, IDictionary<string, string> routeParameters)
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
        /// get the web request
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="routeParameters"></param>
        /// <returns></returns>
        public static async Task<object> DoGetAndGetRetuanValue(string requestUrl, IDictionary<string, string> routeParameters)
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
    }
}
