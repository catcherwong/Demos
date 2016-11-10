using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace JWT.Common.Middlewares.TokenProvider
{
    public static class TokenProviderExtensions
    {        
        public static IApplicationBuilder UseTokenProvider(this IApplicationBuilder builder, TokenProviderOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return builder.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));            
        }
    }
}
