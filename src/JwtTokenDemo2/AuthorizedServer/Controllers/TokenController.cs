using AuthorizedServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthorizedServer.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        //some config in the appsettings.json
        private IOptions<Audience> _settings;
        //repository to handler the sqlite database
        private IRTokenRepository _repo;

        public TokenController(IOptions<Audience> settings, IRTokenRepository repo)
        {
            this._settings = settings;
            this._repo = repo;
        }

        [HttpGet("auth")]
        public IActionResult Auth([FromQuery]Parameters parameters)
        {
            if (parameters == null)
            {
                return Json(new ResponseData
                {
                    Code = "901",
                    Message = "null of parameters",
                    Data = null
                });
            }

            if (parameters.grant_type == "password")
            {
                return Json(DoPassword(parameters));
            }
            else if (parameters.grant_type == "refresh_token")
            {
                return Json(DoRefreshToken(parameters));
            }
            else
            {
                return Json(new ResponseData
                {
                    Code = "904",
                    Message = "bad request",
                    Data = null
                });
            }
        }

        //scenario 1 ： get the access-token by username and password
        private ResponseData DoPassword(Parameters parameters)
        {
            //validate the client_id/client_secret/username/password                                          
            var isValidated = UserInfo.GetAllUsers().Any(x => x.ClientId == parameters.client_id
                                    && x.ClientSecret == parameters.client_secret
                                    && x.UserName == parameters.username
                                    && x.Password == parameters.password);

            if (!isValidated)
            {
                return new ResponseData
                {
                    Code = "902",
                    Message = "invalid user infomation",
                    Data = null
                };
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var rToken = new RToken
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0
            };

            //store the refresh_token 
            if (_repo.AddToken(rToken))
            {
                return new ResponseData
                {
                    Code = "999",
                    Message = "OK",
                    Data = GetJwt(parameters.client_id, refresh_token)
                };
            }
            else
            {
                return new ResponseData
                {
                    Code = "909",
                    Message = "can not add token to database",
                    Data = null
                };
            }
        }

        //scenario 2 ： get the access_token by refresh_token
        private ResponseData DoRefreshToken(Parameters parameters)
        {
            var token = _repo.GetToken(parameters.refresh_token, parameters.client_id);

            if (token == null)
            {
                return new ResponseData
                {
                    Code = "905",
                    Message = "can not refresh token",
                    Data = null
                };
            }

            if (token.IsStop == 1)
            {
                return new ResponseData
                {
                    Code = "906",
                    Message = "refresh token has expired",
                    Data = null
                };
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            token.IsStop = 1;
            //expire the old refresh_token and add a new refresh_token
            var updateFlag = _repo.ExpireToken(token);

            var addFlag = _repo.AddToken(new RToken
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0
            });

            if (updateFlag && addFlag)
            {
                return new ResponseData
                {
                    Code = "999",
                    Message = "OK",
                    Data = GetJwt(parameters.client_id, refresh_token)
                };
            }
            else
            {
                return new ResponseData
                {
                    Code = "910",
                    Message = "can not expire token or a new token",
                    Data = null
                };
            }
        }
        
        private string GetJwt(string client_id, string refresh_token)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client_id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = _settings.Value.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds,
                refresh_token = refresh_token,
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
