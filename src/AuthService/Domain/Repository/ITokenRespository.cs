using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entity;

namespace AuthService.Domain.Repository
{
    public interface ITokenRespository
    {
        void Add(TokenEntity token);
        void Delete(TokenEntity token);
        Task<TokenEntity?> GetByRefreshTokenAsync(string RefreshToken);
    }
}