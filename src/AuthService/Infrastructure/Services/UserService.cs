using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Domain;
using AuthService.Domain.Core.Result;
using AuthService.Domain.Errors;
using AuthService.Domain.Repository;
using AuthService.Interface;
using AuthService.Interface.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;          
        private readonly IUnitOfWork _unitOfWork;
        
         private readonly ILogger<UserService> _logger;
       public UserService(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger
          )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _unitOfWork = unitOfWork;

            _unitOfWork = unitOfWork;

            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateUser(CreateUserRequest request)
        {
            var userExist = await _userRepository.GetByEmailAsync(request.Email);

            if (userExist != null)
            {
                _logger.LogWarning("Attempt to create a user with an existing email: {Email}", request.Email);
                return Result.Failure<Guid>(DomainErrors.User.EmailNotUnique);
            }

            var user = UserEntity.Create(               
                request.Name,              
                request.Email,
                _passwordHasher.HashPassword(request.Password)
            );

            if (user.IsFailure)
            {
                _logger.LogError("User creation failed for email: {Email}. Error: {Error}", request.Email, user.Error);
                return Result.Failure(user.Error);
            }             

            _userRepository.Add(user.Value);            

            await _unitOfWork.SaveChangesAsync();
            
             _logger.LogInformation("User created successfully: {Email}", request.Email);

            return Result.Success(user.Value.Id);
        }
    }
}