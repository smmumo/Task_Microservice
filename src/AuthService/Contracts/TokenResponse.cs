using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Contracts
{
    public record TokenResponse(string Refresh_Token, string Access_Token);
}