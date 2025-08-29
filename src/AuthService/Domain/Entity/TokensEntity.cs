using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuthService.Domain.Entity
{
    public class TokenEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        //public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

        public static TokenEntity Create(string refreshToken , string email)
        {
            TokenEntity tokens = new()
            {
                Email = email,
                RefreshToken = refreshToken,
            };
            return tokens;
        }
    }
}