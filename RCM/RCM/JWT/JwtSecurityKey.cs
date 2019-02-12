using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCM.JWT
{
    public static class JwtSecurityKey
    {
        public const string JwtKey = "UkNNLUFQSS1UaG9uZy1HYXRld2F5";//RCM-API-Thong-Gateway
        public const string JwtIssuer = "https://rcm-api-gateway.com";
        public const double JwtExpireDays = 30;
        public static SymmetricSecurityKey Create()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtKey));
        }
    }
}
