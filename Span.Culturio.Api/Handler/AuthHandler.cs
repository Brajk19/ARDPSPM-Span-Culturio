using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Span.Culturio.Api.Handler
{
    public class AuthHandler : IAuthHandler
    {
        private static class AuthClaimTypes
        {
            public const string Account = "AccountId";
        }

        public string CreateToken(string username, string userId, string jwtKey)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public int GetAccountIdFromClaim(ClaimsIdentity claimsIdentity)
        {
            var accountClaim = claimsIdentity.FindFirst(AuthClaimTypes.Account);
            if (accountClaim is null)
                return -1;

            _ = int.TryParse(accountClaim.Value, out int accountId);
            return accountId;
        }
    }
}
