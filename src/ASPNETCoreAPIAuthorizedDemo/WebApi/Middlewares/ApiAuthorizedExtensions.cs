using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace WebApi.Middlewares
{
    public static class ApiAuthorizedExtensions
    {
        public static IApplicationBuilder UseApiAuthorized(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ApiAuthorizedMiddleware>();
        }

        public static IApplicationBuilder UseApiAuthorized(this IApplicationBuilder builder, ApiAuthorizedOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return builder.UseMiddleware<ApiAuthorizedMiddleware>(Options.Create(options));
        }
    }
}
