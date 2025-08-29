using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Domain.Core.Result;

namespace AuthService.Interface
{
    public interface IAuthenticationService
    {
        Task<Result<TokenResponse>> Aunthenticate(LoginRequest request);
        Task<Result<TokenResponse>> GetRefreshToken(RefreshTokenRequest request);
        Task<Result> Logout(RefreshTokenRequest request);
    }
}