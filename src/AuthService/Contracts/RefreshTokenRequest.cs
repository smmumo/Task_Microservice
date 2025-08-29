using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Contracts
{
    public record RefreshTokenRequest(string Access_Token, string Refresh_Token);
}