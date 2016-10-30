using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace WebApi.Middlewares
{
    public static class DapperExtensions
    {
        public static IApplicationBuilder UseDapper(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ApiAuthorizedMiddleware>();
        }

        public static IApplicationBuilder UseDapper(this IApplicationBuilder builder, DapperOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return builder.UseMiddleware<DapperMiddleWare>(Options.Create(options));
        }
    }
}
