
using AuthService.Contracts;

namespace  AuthService.Interface;
public interface ITokenService
{
    //Task AddUserRefreshTokens(string RefreshToken , string Email);
    Task DeleteUserRefreshTokensAsync(string RefreshToken);
    Task<TokenResponse?> GetSavedRefreshTokens(string RefreshToken);
}