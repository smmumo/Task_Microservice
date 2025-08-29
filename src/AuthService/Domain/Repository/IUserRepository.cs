using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Domain.Repository
{
    public interface IUserRepository
    {
        void Add(UserEntity user);
        void Delete(UserEntity user);
        Task<UserEntity> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}