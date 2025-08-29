using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Contracts
{
    public record LoginRequest(string Email, string Password);
}