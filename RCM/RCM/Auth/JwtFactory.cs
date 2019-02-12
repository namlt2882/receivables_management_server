using RCM.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RCM.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                 //new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                 //new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 //new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Role),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
             };

            //var _claims = new Dictionary<string, string>();
            //_claims.Add(identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Role).Type, identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Role).Value);
            //_claims.Add(identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id).Type, identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id).Value);

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create())
                                .AddSubject("RCM-API")
                                .AddIssuer(_jwtOptions.Issuer)
                                .AddAudience(_jwtOptions.Audience)
                                .AddClaim(JwtRegisteredClaimNames.UniqueName, userName)
                                .AddClaims(claims)
                                //.AddClaims(_claims)
                                .AddExpiry(100)
                                .Build();

            return token.Value;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id, string role)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Role, role),
                //new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
