using System.Security.Claims;

namespace NewsAgregator.WebAPI.Auth
{
    public interface IJwtAuthManager
    {
        JwtAuthResult GenerateTokens(string email, Claim[] claims);
    }
}