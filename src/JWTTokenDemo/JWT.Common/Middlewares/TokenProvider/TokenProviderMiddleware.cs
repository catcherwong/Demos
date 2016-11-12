using JWT.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace JWT.Common.Middlewares.TokenProvider
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;        
        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options)
        {
            _next = next;
            _options = options.Value;            
        }    

        /// <summary>
        /// invoke the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {           
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                await _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                await ReturnBadRequest(context);             
            }

            await GenerateAuthorizedResult(context);
        }

        /// <summary>
        /// get the result of authorized
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GenerateAuthorizedResult(HttpContext context)
        {         
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                await ReturnBadRequest(context);
                return;
            }            
            
            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(GetJwt(username));
        }

        /// <summary>
        /// validate the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var list = ApplicationInfo.GetAllApplication();

            bool isValidated = list.Count(x => x.ApplicationName == username && x.ApplicationPassword == password)==1;
            
            if (isValidated)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }

        /// <summary>
        /// return the bad request (400)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnBadRequest(HttpContext context)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = "invalid_grant",
                error_description = "Audience validation failed"
            }));
        }

        /// <summary>
        /// get the jwt
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GetJwt(string username)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                          ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds,
                token_type = "Bearer"
            };            

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

    }
}
