using System.Security.Claims;

namespace Span.Culturio.Api.Handler
{
    public interface IAuthHandler
    {
        string CreateToken(string username, string userId, string jwtKey);
        int GetAccountIdFromClaim(ClaimsIdentity claimsIdentity);
    }
}
