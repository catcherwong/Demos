using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WebAPI
{
    public partial class Startup
    {
        public void ConfigureJwtAuth(IApplicationBuilder app)
        {            
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray);            

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "http://catcher1994.cnblogs.com/",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "Catcher Wong",

                // Validate the token expiry
                ValidateLifetime = true,
         
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
            });                        
        }
    }
}
