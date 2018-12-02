namespace TplDemo.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public RequestLogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogMiddleware>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var requestId = Guid.NewGuid().ToString("N");
            context.Items.Add("requestId", requestId);

            //request log 
            var reqMsg = await FormatRequest(context.Request);
            _logger.LogInformation($"{requestId},Path={context.Request.Path},QueryString={context.Request.QueryString.ToString()},Body={reqMsg}");

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                var sp = new Stopwatch();
                sp.Start();
                await _next(context);
                sp.Stop();
                //response log
                var resMsg = await FormatResponse(context.Response);
                _logger.LogInformation($"{requestId},Cost={sp.ElapsedMilliseconds}ms,Response={resMsg}");
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            request.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return text?.Trim().Replace("\r", "").Replace("\n", "");
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            if (response.HasStarted) return string.Empty;
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text?.Trim().Replace("\r", "").Replace("\n", "");
        }
    }
}
