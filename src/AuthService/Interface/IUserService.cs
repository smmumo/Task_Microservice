using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Domain.Core.Result;

namespace AuthService.Interface
{
    public interface IUserService
    {
        Task<Result> CreateUser(CreateUserRequest request);
    }
}