using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Core.Result;
using AuthService.Domain.Errors;

namespace AuthService.Domain
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public static UserEntity Create(string name, string email, string password)
        {    
            UserEntity user = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password
            };
            return user;
        }        

        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}