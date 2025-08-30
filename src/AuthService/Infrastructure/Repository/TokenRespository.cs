using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entity;
using AuthService.Domain.Repository;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repository
{
    public class TokenRespository : ITokenRespository
    {
        private readonly AuthDbContext _dbContext;

        public TokenRespository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TokenEntity?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.Set<TokenEntity>().FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        }

        public void Add(TokenEntity token)
        {
            _dbContext.Set<TokenEntity>().Add(token);
        }

        public void Delete(TokenEntity token)
        {
            _dbContext.Set<TokenEntity>().Remove(token);
        }     
       
    }
}