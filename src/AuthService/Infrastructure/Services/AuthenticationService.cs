using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Domain;
using AuthService.Domain.Core.Result;
using AuthService.Domain.Entity;
using AuthService.Domain.Errors;
using AuthService.Domain.Repository;
using AuthService.Interface;

namespace AuthService.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashChecker _passwordHashChecker;
    private readonly IJwtProvider _jwtprovider;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenRespository _tokenRespository;
    private readonly ILogger<AuthenticationService> _logger;
    public AuthenticationService(IUserRepository userRepository,
        IPasswordHashChecker passwordHashChecker,
        IJwtProvider jwtprovider,
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        ITokenRespository tokenRespository,
        ILogger<AuthenticationService> logger)
    {
        _userRepository = userRepository;
        _passwordHashChecker = passwordHashChecker;
        _jwtprovider = jwtprovider;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _tokenRespository = tokenRespository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<TokenResponse>> Aunthenticate(LoginRequest request)
    {
        UserEntity? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            _logger.LogWarning("Authentication failed for email: {Email}. User not found.", request.Email);
            return Result.Failure<TokenResponse>(DomainErrors.User.NotFound);
        }

        bool passwordMatches = _passwordHashChecker.HashesMatch(user.Password, request.Password);

        if (!passwordMatches)
        {
            _logger.LogWarning("Authentication failed for email: {Email}. Invalid password.", request.Email);
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
        }

        var token = _jwtprovider.GenerateToken(request.Email);

        // saving refresh token to the db
        if (!string.IsNullOrEmpty(token.Refresh_Token))
        {
            var refreshToken = TokenEntity.Create(token.Refresh_Token, request.Email);
            _tokenRespository.Add(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }


        return Result.Success(new TokenResponse(token.Access_Token, token.Refresh_Token));

    }

    public async Task<Result<TokenResponse>> GetRefreshToken(RefreshTokenRequest request)
    {
        var principal = _jwtprovider.GetPrincipalFromExpiredToken(request.Access_Token);

        var Email = principal.Identity?.Name;
        if (Email is null)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidToken);
        }

        var savedRefreshToken = await _tokenService.GetSavedRefreshTokens(request.Refresh_Token);

        if (savedRefreshToken is null)
        {
            _logger.LogWarning("Refresh token not found or invalid for email: {Email}.", Email);
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidToken);
        }

        if (savedRefreshToken.Refresh_Token != request.Refresh_Token)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidToken);
        }

        var newJwtToken = _jwtprovider.GenerateRefreshToken(Email);

        var previousStoredTokenEntity = await _tokenRespository.GetByRefreshTokenAsync(request.Refresh_Token);

        //delete  previous token
        _tokenRespository.Delete(previousStoredTokenEntity!);

        // saving refresh token to the db  
        var refreshToken = TokenEntity.Create(newJwtToken.Refresh_Token, Email);


        _tokenRespository.Add(refreshToken);
        
        _logger.LogInformation("Refresh token generated successfully for email: {Email}.", Email);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(newJwtToken);

    }
    
    public async Task<Result> Logout(RefreshTokenRequest request)
    {
        var principal = _jwtprovider.GetPrincipalFromExpiredToken(request.Access_Token);

        var Email = principal.Identity?.Name;

        if (Email is null)
        {
            return Result.Failure(DomainErrors.Authentication.InvalidToken);
        }

        var previousStoredTokenEntity = await _tokenRespository.GetByRefreshTokenAsync(request.Refresh_Token);

        if (previousStoredTokenEntity is null)
        {
            return Result.Failure(DomainErrors.Authentication.InvalidToken);
        }    

         _tokenRespository.Delete(previousStoredTokenEntity!);

         await _unitOfWork.SaveChangesAsync();

         return Result.Success();
    }
    
}