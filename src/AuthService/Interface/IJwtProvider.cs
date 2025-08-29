
using System.Security.Claims;
using AuthService.Contracts;
namespace AuthService.Interface;
/// <summary>
/// Represents the JWT provider interface.
/// </summary>
public interface IJwtProvider
{     
    TokenResponse GenerateToken(string Email);
    TokenResponse GenerateRefreshToken(string Email);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

