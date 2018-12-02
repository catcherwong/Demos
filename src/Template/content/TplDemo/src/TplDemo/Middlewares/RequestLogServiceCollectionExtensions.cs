namespace TplDemo.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Request log service collection extensions.
    /// </summary>
    public static class RequestLogServiceCollectionExtensions
    {
        /// <summary>
        /// Uses the request log.
        /// </summary>
        /// <returns>The request log.</returns>
        /// <param name="builder">Builder.</param>
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
