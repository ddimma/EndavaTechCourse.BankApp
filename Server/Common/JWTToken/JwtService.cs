using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndavaTechCourse.BankApp.Server.Common.JWTToken
{
    public class JwtService
    {
        public readonly TokenSettings tokenSettings;

        public JwtService(TokenSettings tokenSettings)
        {
            ArgumentNullException.ThrowIfNull(tokenSettings);

            this.tokenSettings = tokenSettings;
        }

        public string CreateAuthToken(string userId, string username, string[] roles)
        {
            List<Claim> claims = new()
        {
            new(Constants.UserIdClaimName, userId),
            new(Constants.UsernameClaimName, username)
        };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(tokenSettings.ExpirationInMinutes)),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}

