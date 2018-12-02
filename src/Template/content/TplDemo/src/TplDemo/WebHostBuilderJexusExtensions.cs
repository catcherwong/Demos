namespace TplDemo
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 用于处理客户IP地址、端口的HostBuilder中间件
    /// </summary>
    public static class WebHostBuilderJexusExtensions
    {
        /// <summary>
        /// 启用JexusIntegration中间件
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseJexusIntegration(this IWebHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            // 检查是否已经加载过了
            if (hostBuilder.GetSetting(nameof(UseJexusIntegration)) != null)
            {
                return hostBuilder;
            }


            // 设置已加载标记，防止重复加载
            hostBuilder.UseSetting(nameof(UseJexusIntegration), true.ToString());


            // 添加configure处理
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new JwsSetupFilter());
            });


            return hostBuilder;
        }
    }

    class JwsSetupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<JexusMiddleware>();
                next(app);
            };
        }
    }

    class JexusMiddleware
    {
        readonly RequestDelegate _next;
        public JexusMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<IISOptions> options)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;

            try
            {
                if (headers != null && headers.ContainsKey("X-Original-For"))
                {
                    var ipaddAdndPort = headers["X-Original-For"].ToArray()[0];
                    var dot = ipaddAdndPort.IndexOf(":", StringComparison.Ordinal);
                    var ip = ipaddAdndPort;
                    var port = 0;
                    if (dot > 0)
                    {
                        ip = ipaddAdndPort.Substring(0, dot);
                        port = int.Parse(ipaddAdndPort.Substring(dot + 1));
                    }

                    httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse(ip);
                    if (port != 0) httpContext.Connection.RemotePort = port;
                }
            }
            finally
            {
                await _next(httpContext);
            }
        }
    }
}
