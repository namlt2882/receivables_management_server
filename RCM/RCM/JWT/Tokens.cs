using RCM.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RCM.JWT
{
    public class Tokens
    {
        public static async Task<object> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                username = userName,
                role = identity.Claims.Single(c => c.Type == "role").Value,
                access_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
            };

            return response;
        }
    }
}
