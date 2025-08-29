using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Contracts
{
    public record CreateUserRequest(
        string Name,
        string Email,
        string Password,
        string Role,
        string ConfirmPassword);
}